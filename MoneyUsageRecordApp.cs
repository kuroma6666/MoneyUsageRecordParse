using SonyBankUsageRecordParse.src.subsystems.LoadCSV;
using System.Diagnostics;
using System.Globalization;

namespace SonyBankUsageRecordParse
{
	public partial class MoneyUsageRecordApp : Form
	{
		private ComboBox comboBoxExpenseCategory;
		private List<Transaction> transactions = new List<Transaction>();
		public class Transaction
		{
			public string StoreName { get; set; }
			public decimal Amount { get; set; }
			public string ExpenseCategory { get; set; }
		}

		public MoneyUsageRecordApp()
		{
			InitializeComponent();
			SetupComboBox();

		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void buttoParseCSVFile_Click(object sender, EventArgs e)
		{
			String csvFilePath = SelectCSVFile();
			if (csvFilePath != null)
			{
				Debug.WriteLine($"選択されたファイル: {csvFilePath}");
			}
			else
			{
				Debug.WriteLine("CSVファイルが選択されませんでした。");
				return;
			}
			var parser = new SonyBankCSV();
			Debug.WriteLine("Parse CSV Start");
			var transactions = parser.ParseCSV(csvFilePath);
			Debug.WriteLine("Parse CSV End");
			// パースした取引明細を表示
			foreach (var transaction in transactions)
			{
				Debug.WriteLine(transaction);
				var listViewItem = new ListViewItem(transaction.Date.ToString("yyyy-MM-dd"));
				listViewItem.SubItems.Add(transaction.StoreName);
				listViewItem.SubItems.Add(transaction.Amount.ToString("C", CultureInfo.CurrentCulture));
				listViewItem.SubItems.Add(transaction.Balance.ToString("C", CultureInfo.CurrentCulture));

				transactionListView.Items.Add(listViewItem); // 利用明細ListViewに追加

				String expenseCategoryDefault = " ";
				ListViewItem expenseItem = new ListViewItem(new[] { expenseCategoryDefault, transaction.StoreName, transaction.Amount.ToString("C", CultureInfo.CurrentCulture) });
				listViewExpenseRegistration.Items.Add(expenseItem); // 費用項目登録用ListViewに追加
			}

		}
		private String SelectCSVFile()
		{
			using OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
				Title = "CSVファイルを選択してください"
			};
			return openFileDialog.ShowDialog() == DialogResult.OK ? openFileDialog.FileName : null;
		}
		private void SetupComboBox()
		{
			String[] expenseCategories = { " ", "食費", "外食費", "被服費", "娯楽費", "日用品・雑貨費", "サブスクリプション費" };
			// ComboBoxの設定
			comboBoxExpenseCategory = new ComboBox
			{
				DropDownStyle = ComboBoxStyle.DropDownList,
				Location = new Point(420, 50),
				Size = new Size(120, 30)
			};
			comboBoxExpenseCategory.Items.AddRange(expenseCategories);
			comboBoxExpenseCategory.SelectedIndexChanged += comboBoxExpenseCategory_SelectedIndexChanged;
			this.Controls.Add(comboBoxExpenseCategory);
			// Setup時点では費用項目選択コンボボックスはInVisibleで良い
			comboBoxExpenseCategory.Visible = false;
		}

		private void buttonLinkStoreToExpense_Click(object sender, EventArgs e)
		{

			// 費用項目登録用ListViewからデータを統計ListViewに移す
			foreach (ListViewItem item in listViewExpenseRegistration.Items)
			{
				if (comboBoxExpenseCategory.SelectedItem != null && !String.IsNullOrEmpty(item.SubItems[1].Text))
				{
					String category = comboBoxExpenseCategory.SelectedItem.ToString();
					String storeName = item.SubItems[0].Text;
					Decimal amount = Decimal.Parse(item.SubItems[1].Text);

					// 登録されたデータをリストに保存
					transactions.Add(new Transaction
					{
						StoreName = storeName,
						Amount = amount,
						ExpenseCategory = category
					});

					// 統計リストに集計
					var statItem = listViewExpenseStatistics.Items.Cast<ListViewItem>()
							.FirstOrDefault(i => i.SubItems[0].Text == category);

					if (statItem != null)
					{
						// 既存のカテゴリがあれば合計を更新
						Decimal total = Decimal.Parse(statItem.SubItems[1].Text) + amount;
						statItem.SubItems[1].Text = total.ToString();
					}
					else
					{
						// 新しいカテゴリとして追加
						listViewExpenseStatistics.Items.Add(new ListViewItem(new[] { category, amount.ToString() }));
					}

					// 統計に反映後、費用項目登録用ListViewから削除
					item.Remove();
				}
			}
		}

		private void listViewExpenseRegistration_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listViewExpenseRegistration.SelectedItems.Count > 0)
			{
				var selectedItem = listViewExpenseRegistration.SelectedItems[0];
				var itemBounds = selectedItem.GetBounds(ItemBoundsPortion.Label);

				var expenseCategoryColumnLocation = transactionListView.Columns[0].Width;
				var expenseCategoryColumnWidth = transactionListView.Columns[0].Width;
				comboBoxExpenseCategory.Location = new Point(itemBounds.Left + 20, itemBounds.Top + 90);
				comboBoxExpenseCategory.Width = expenseCategoryColumnWidth + 15;

				comboBoxExpenseCategory.Visible = true;
				comboBoxExpenseCategory.BringToFront();
				comboBoxExpenseCategory.Text = selectedItem.SubItems[0].Text;


			}
		}

		private void comboBoxExpenseCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 選択されている ListView のアイテムを取得
			if (listViewExpenseRegistration.SelectedItems.Count > 0)
			{
				var selectedItem = transactionListView.SelectedItems[0];

				// 選択された費用項目を取得
				var selectedExpenseCategory = comboBoxExpenseCategory.SelectedItem.ToString();

				// ListViewItem の内容を更新
				selectedItem.SubItems[0].Text = selectedExpenseCategory; // 費用項目を更新
			}
		}

	}
}
