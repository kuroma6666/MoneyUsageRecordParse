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
				Debug.WriteLine($"�I�����ꂽ�t�@�C��: {csvFilePath}");
			}
			else
			{
				Debug.WriteLine("CSV�t�@�C�����I������܂���ł����B");
				return;
			}
			var parser = new SonyBankCSV();
			Debug.WriteLine("Parse CSV Start");
			var transactions = parser.ParseCSV(csvFilePath);
			Debug.WriteLine("Parse CSV End");
			// �p�[�X����������ׂ�\��
			foreach (var transaction in transactions)
			{
				Debug.WriteLine(transaction);
				var listViewItem = new ListViewItem(transaction.Date.ToString("yyyy-MM-dd"));
				listViewItem.SubItems.Add(transaction.StoreName);
				listViewItem.SubItems.Add(transaction.Amount.ToString("C", CultureInfo.CurrentCulture));
				listViewItem.SubItems.Add(transaction.Balance.ToString("C", CultureInfo.CurrentCulture));

				transactionListView.Items.Add(listViewItem); // ���p����ListView�ɒǉ�

				String expenseCategoryDefault = " ";
				ListViewItem expenseItem = new ListViewItem(new[] { expenseCategoryDefault, transaction.StoreName, transaction.Amount.ToString("C", CultureInfo.CurrentCulture) });
				listViewExpenseRegistration.Items.Add(expenseItem); // ��p���ړo�^�pListView�ɒǉ�
			}

		}
		private String SelectCSVFile()
		{
			using OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
				Title = "CSV�t�@�C����I�����Ă�������"
			};
			return openFileDialog.ShowDialog() == DialogResult.OK ? openFileDialog.FileName : null;
		}
		private void SetupComboBox()
		{
			String[] expenseCategories = { " ", "�H��", "�O�H��", "�핞��", "��y��", "���p�i�E�G�ݔ�", "�T�u�X�N���v�V������" };
			// ComboBox�̐ݒ�
			comboBoxExpenseCategory = new ComboBox
			{
				DropDownStyle = ComboBoxStyle.DropDownList,
				Location = new Point(420, 50),
				Size = new Size(120, 30)
			};
			comboBoxExpenseCategory.Items.AddRange(expenseCategories);
			comboBoxExpenseCategory.SelectedIndexChanged += comboBoxExpenseCategory_SelectedIndexChanged;
			this.Controls.Add(comboBoxExpenseCategory);
			// Setup���_�ł͔�p���ڑI���R���{�{�b�N�X��InVisible�ŗǂ�
			comboBoxExpenseCategory.Visible = false;
		}

		private void buttonLinkStoreToExpense_Click(object sender, EventArgs e)
		{

			// ��p���ړo�^�pListView����f�[�^�𓝌vListView�Ɉڂ�
			foreach (ListViewItem item in listViewExpenseRegistration.Items)
			{
				if (comboBoxExpenseCategory.SelectedItem != null && !String.IsNullOrEmpty(item.SubItems[1].Text))
				{
					String category = comboBoxExpenseCategory.SelectedItem.ToString();
					String storeName = item.SubItems[0].Text;
					Decimal amount = Decimal.Parse(item.SubItems[1].Text);

					// �o�^���ꂽ�f�[�^�����X�g�ɕۑ�
					transactions.Add(new Transaction
					{
						StoreName = storeName,
						Amount = amount,
						ExpenseCategory = category
					});

					// ���v���X�g�ɏW�v
					var statItem = listViewExpenseStatistics.Items.Cast<ListViewItem>()
							.FirstOrDefault(i => i.SubItems[0].Text == category);

					if (statItem != null)
					{
						// �����̃J�e�S��������΍��v���X�V
						Decimal total = Decimal.Parse(statItem.SubItems[1].Text) + amount;
						statItem.SubItems[1].Text = total.ToString();
					}
					else
					{
						// �V�����J�e�S���Ƃ��Ēǉ�
						listViewExpenseStatistics.Items.Add(new ListViewItem(new[] { category, amount.ToString() }));
					}

					// ���v�ɔ��f��A��p���ړo�^�pListView����폜
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
			// �I������Ă��� ListView �̃A�C�e�����擾
			if (listViewExpenseRegistration.SelectedItems.Count > 0)
			{
				var selectedItem = transactionListView.SelectedItems[0];

				// �I�����ꂽ��p���ڂ��擾
				var selectedExpenseCategory = comboBoxExpenseCategory.SelectedItem.ToString();

				// ListViewItem �̓��e���X�V
				selectedItem.SubItems[0].Text = selectedExpenseCategory; // ��p���ڂ��X�V
			}
		}

	}
}
