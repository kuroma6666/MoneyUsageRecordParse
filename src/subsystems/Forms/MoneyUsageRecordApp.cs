using System.Diagnostics;
using System.Globalization;

using SonyBankUsageRecordParse.src;
using SonyBankUsageRecordParse.src.subsystems.CSV.Common;
using SonyBankUsageRecordParse.src.subsystems.Expense;
using SonyBankUsageRecordParse.src.subsystems.Transactions;
namespace SonyBankUsageRecordParse
{
	public partial class MoneyUsageRecordApp : Form
	{
		private ComboBox comboBoxExpenseCategory;
		private List<ExpenseTransaction> transactions = new();
		private Boolean drawWokerRunning = true;
		private const UInt16 DEF_BG_SLEEP = 10000;
		private TransactionManager transactionManager = new();
		private ExpenseCategoryManager expenseCategoryManager;

		public MoneyUsageRecordApp()
		{
			InitializeComponent();
			expenseCategoryManager = new ExpenseCategoryManager(new JsonExpenseCategoryProvider("config\\categories.json"));
			SetupComboBox();
		}

		private void MoneyUsageRecordApp_Load(object sender, EventArgs e)
		{
			backgroundWorkerDraw.RunWorkerAsync();
		}

		private void SetupComboBox()
		{
			comboBoxExpenseCategory = new ComboBox
			{
				DropDownStyle = ComboBoxStyle.DropDownList,
				Location = new Point(420, 50),
				Size = new Size(120, 30),
				BackColor = listViewExpenseRegistration.BackColor,
				ForeColor = listViewExpenseRegistration.ForeColor
			};

			var categories = expenseCategoryManager.GetCategories();
			foreach (var category in categories)
			{
				comboBoxExpenseCategory.Items.Add(category);
			}

			comboBoxExpenseCategory.SelectedIndexChanged += ComboBoxExpenseCategory_SelectedIndexChanged;
			this.Controls.Add(comboBoxExpenseCategory);
			comboBoxExpenseCategory.Visible = false;
		}

		private void ButtoParseCSVFile_Click(object sender, EventArgs e)
		{
			String csvFilePath = CSVUtilitiy.SelectCSVFile();
			if (csvFilePath != null)
			{
				Debug.WriteLine($"選択されたCSVファイル: {csvFilePath}");
			}
			else
			{
				Debug.WriteLine("CSVファイルが選択されませんでした。");
				return;
			}

			if (comboBoxCSVType.SelectedItem == null)
			{
				MessageBox.Show("CSV種別を選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var selectedCSVType = comboBoxCSVType.SelectedItem.ToString() switch
			{
				"SonyBank v2" => CSVType.SonyBankv2,
				"SMBC" => CSVType.SMBC,
				"SonyBank v1" => CSVType.SonyBankv1,
				_ => throw new InvalidOperationException("無効なCSV種別が選択されました。")
			};

			transactionManager.ParseCSVFile(csvFilePath, selectedCSVType);
			Debug.WriteLine("Parse CSV End");

			foreach (var transaction in transactionManager.Transactions)
			{
				Debug.WriteLine(transaction);
				var listViewItem = new ListViewItem(transaction.Date.ToString("yyyy-MM-dd"));
				listViewItem.SubItems.Add(transaction.StoreName);
				listViewItem.SubItems.Add(transaction.Amount.ToString("C", CultureInfo.CurrentCulture));
				listViewItem.SubItems.Add(transaction.Balance.ToString("C", CultureInfo.CurrentCulture));

				transactionListView.Items.Add(listViewItem);

				String expenseCategoryDefault = ExpenseCategory.None.ToFriendlyString();
				ListViewItem expenseItem = new ListViewItem(
						new[] { expenseCategoryDefault, transaction.StoreName, transaction.Amount.ToString("C", CultureInfo.CurrentCulture) }
				);
				listViewExpenseRegistration.Items.Add(expenseItem);
			}

			var storeNameTags = transactionManager.LoadStoreNameTags();
			if (storeNameTags != null)
			{
				UpdateListViewExpenseRegistration(storeNameTags);
			}
		}

		private void ButtonGenerateStatistics_Click(object sender, EventArgs e)
		{
			listViewExpenseStatistics.Items.Clear();

			foreach (ListViewItem item in listViewExpenseRegistration.Items)
			{
				if (item.SubItems[0].Text.Contains(" ")) continue;
				if (!String.IsNullOrEmpty(item.SubItems[1].Text))
				{
					String category = item.SubItems[0].Text;
					String amountText = item.SubItems[2].Text;
					Decimal amount = Decimal.Parse(amountText, NumberStyles.Currency, CultureInfo.CurrentCulture);
					Debug.WriteLine(amount);

					var statItem = listViewExpenseStatistics.Items.Cast<ListViewItem>()
							.FirstOrDefault(i => i.SubItems[0].Text == category);

					if (statItem != null)
					{
						String totalText = statItem.SubItems[1].Text;
						Decimal totalAmount = Decimal.Parse(totalText, NumberStyles.Currency, CultureInfo.CurrentCulture);
						totalAmount += amount;
						statItem.SubItems[1].Text = totalAmount.ToString("C", CultureInfo.CurrentCulture);
					}
					else
					{
						listViewExpenseStatistics.Items.Add(new ListViewItem(new[] { category, amountText.ToString() }));
					}
				}
			}
		}

		private void buttonLinkStoreExpensPageRegister_Click(object sender, EventArgs e)
		{
			try
			{
				transactionManager.LinkStoreToExpenseCategory(textBox_StoreNameExpenseCategory.Text);
			}
			catch (FormatException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ButtonDisplayStoreNameTagList_Click(object sender, EventArgs e)
		{
			var storeNameTagLists = new StoreNameTagLists();
			storeNameTagLists.Show();
		}

		private void ListViewExpenseRegistration_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listViewExpenseRegistration.SelectedItems.Count > 0)
			{
				var selectedItem = listViewExpenseRegistration.SelectedItems[0];
				var itemBounds = selectedItem.GetBounds(ItemBoundsPortion.Label);
				var expenseCategoryColumnWidth = transactionListView.Columns[0].Width;
				comboBoxExpenseCategory.Location = new Point(itemBounds.Left + 20, itemBounds.Top + 110);
				comboBoxExpenseCategory.Width = expenseCategoryColumnWidth + 15;
				comboBoxExpenseCategory.Visible = true;
				comboBoxExpenseCategory.BringToFront();
				comboBoxExpenseCategory.Text = selectedItem.SubItems[0].Text;

				if (listViewExpenseRegistration.SelectedItems.Count > 0)
				{
					var selectedStoreName = selectedItem.SubItems[1].Text;
					var selectedExpenseCategory = selectedItem.SubItems[0].Text;
					if (selectedExpenseCategory.Contains(" ")) selectedExpenseCategory = "未選択";
					textBox_StoreNameExpenseCategory.Text = $"{selectedStoreName}:{selectedExpenseCategory}";
				}
			}
		}

		private void ComboBoxExpenseCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listViewExpenseRegistration.SelectedItems.Count > 0)
			{
				var selectedItem = listViewExpenseRegistration.SelectedItems[0];
				var selectedExpenseCategory = comboBoxExpenseCategory.SelectedItem.ToString();
				var selectedStoreName = selectedItem.SubItems[1].Text;

				foreach (ListViewItem item in listViewExpenseRegistration.Items)
				{
					if (item.SubItems[1].Text == selectedStoreName)
					{
						item.SubItems[0].Text = selectedExpenseCategory;
					}
				}
				textBox_StoreNameExpenseCategory.Text = $"{selectedStoreName}:{selectedExpenseCategory}";
			}
		}

		private void TabControlListViews_SelectedIndexChanged(object sender, EventArgs e)
		{
			comboBoxExpenseCategory.Visible = false;
		}

		Boolean isUpdatedListViewExpenseRegistrationItems = false;

		private void UpdateListViewExpenseRegistration(List<String> storeNameTags)
		{
			if (listViewExpenseRegistration.InvokeRequired)
			{
				listViewExpenseRegistration.Invoke(new Action(() =>
				{
					UpdateListViewExpenseRegistration(storeNameTags);
				}));
			}
			else
			{
				foreach (var storeNameTag in storeNameTags)
				{
					var parts = storeNameTag.Split(':');
					if (parts.Length == 2)
					{
						var storeName = parts[0];
						var expenseCategory = parts[1];

						listViewExpenseRegistration.SuspendLayout();
						foreach (ListViewItem item in listViewExpenseRegistration.Items)
						{
							if (item.SubItems[1].Text == storeName)
							{
								item.SubItems[0].Text = expenseCategory;
								isUpdatedListViewExpenseRegistrationItems = true;
							}
						}
						listViewExpenseRegistration.ResumeLayout();
					}
				}
			}
		}

		private void backgroundWorkerDraw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			while (drawWokerRunning)
			{
				var storeNameTags = transactionManager.LoadStoreNameTags();
				if (storeNameTags != null)
				{
					UpdateListViewExpenseRegistration(storeNameTags);
				}
				Thread.Sleep(DEF_BG_SLEEP);
				if (backgroundWorkerDraw.CancellationPending)
				{
					e.Cancel = true;
					return;
				}
			}
		}

	}
}
