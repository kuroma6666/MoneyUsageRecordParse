using SonyBankUsageRecordParse.src;
using SonyBankUsageRecordParse.src.subsystems.CSV;
using SonyBankUsageRecordParse.src.subsystems.CSV.Common;
using SonyBankUsageRecordParse.src.subsystems.Tagging;
using SonyBankUsageRecordParse.src.subsystems.Transactions;

using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
namespace SonyBankUsageRecordParse
{
	public partial class MoneyUsageRecordApp : Form
	{
		private ComboBox comboBoxExpenseCategory;
		private List<ExpenseTransaction> transactions = [];
		private Boolean drawWokerRunning = true;
		private const UInt16 DEF_BG_SLEEP = 10000;
		/// <summary>
		/// [[MoneyUsageRecordApp]]�N���X�̃R���X�g���N�^
		/// </summary>
		public MoneyUsageRecordApp()
		{
			InitializeComponent();
			SetupComboBox();
		}

		/// <summary>
		/// [[MoneyUsageRecordApp]]�̓ǂݍ��݃C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MoneyUsageRecordApp_Load(object sender, EventArgs e)
		{
			backgroundWorkerDraw.RunWorkerAsync();
		}

		/// <summary>
		/// �R���{�{�b�N�X�R���g���[���̃Z�b�g�A�b�v
		/// </summary>
		private void SetupComboBox()
		{
			//��p���ڃJ�e�S���̃R���{�{�b�N�X�̏�����
			String[] expenseCategories = [" ", "�H��", "�O�H��", "�핞��", "��y��", "���p�i�E�G�ݔ�", "�T�u�X�N���v�V������"];
			comboBoxExpenseCategory = new ComboBox
			{
				DropDownStyle = ComboBoxStyle.DropDownList,
				Location = new Point(420, 50),
				Size = new Size(120, 30),
				BackColor = listViewExpenseRegistration.BackColor,
				ForeColor = listViewExpenseRegistration.ForeColor
			};
			comboBoxExpenseCategory.Items.AddRange(expenseCategories);
			comboBoxExpenseCategory.SelectedIndexChanged += ComboBoxExpenseCategory_SelectedIndexChanged;
			this.Controls.Add(comboBoxExpenseCategory);
			// Setup���_�ł͔�p���ڑI���R���{�{�b�N�X�͔�\���ŗǂ�
			comboBoxExpenseCategory.Visible = false;
		}

		/// <summary>
		/// CSV�t�@�C���p�[�X�{�^���̃N���b�N�C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtoParseCSVFile_Click(object sender, EventArgs e)
		{
			String csvFilePath = CSVUtilitiy.SelectCSVFile();
			if (csvFilePath != null)
			{
				Debug.WriteLine($"�I�����ꂽCSV�t�@�C��: {csvFilePath}");
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
			// �p�[�X�������p���ׂ𗘗p����ListView�ɓo�^
			foreach (var transaction in transactions)
			{
				Debug.WriteLine(transaction);
				var listViewItem = new ListViewItem(transaction.Date.ToString("yyyy-MM-dd"));
				listViewItem.SubItems.Add(transaction.StoreName);
				listViewItem.SubItems.Add(transaction.Amount.ToString("C", CultureInfo.CurrentCulture));
				listViewItem.SubItems.Add(transaction.Balance.ToString("C", CultureInfo.CurrentCulture));

				transactionListView.Items.Add(listViewItem); // ���p����ListView�ɒǉ�

				String expenseCategoryDefault = " ";
				ListViewItem expenseItem = new ListViewItem(
					[expenseCategoryDefault, transaction.StoreName, transaction.Amount.ToString("C", CultureInfo.CurrentCulture)]
					);
				listViewExpenseRegistration.Items.Add(expenseItem); // ��p���ړo�^�pListView�ɒǉ�
			}
			StoreNameTagConfig storeConfig = new StoreNameTagConfig();
			storeConfig.Load();
			var storeNameTags = storeConfig.StoreNameTags;
			if (storeNameTags != null)
			{
				UpdateListViewExpenseRegistration(storeNameTags);
			}
		}

		/// <summary>
		/// ���v�����{�^���̃N���b�N�C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonGenerateStatistics_Click(object sender, EventArgs e)
		{
			listViewExpenseStatistics.Items.Clear();

			// ��p���ړo�^�pListView����f�[�^�𓝌vListView�ɂ܂Ƃ߂�
			foreach (ListViewItem item in listViewExpenseRegistration.Items)
			{
				if (item.SubItems[0].Text.Contains(" ")) continue;
				if (!String.IsNullOrEmpty(item.SubItems[1].Text))
				{
					String category = item.SubItems[0].Text;
					String amountText = item.SubItems[2].Text;
					Decimal amount = Decimal.Parse(amountText, NumberStyles.Currency, CultureInfo.CurrentCulture);
					Debug.WriteLine(amount);

					// ���v���X�g�ɏW�v
					var statItem = listViewExpenseStatistics.Items.Cast<ListViewItem>()
							.FirstOrDefault(i => i.SubItems[0].Text == category);

					if (statItem != null)
					{
						// �����J�e�S��������΍��v���X�V
						String totalText = statItem.SubItems[1].Text;
						Decimal totalAmount = Decimal.Parse(totalText, NumberStyles.Currency, CultureInfo.CurrentCulture);
						totalAmount += amount;
						statItem.SubItems[1].Text = totalAmount.ToString("C", CultureInfo.CurrentCulture);
					}
					else
					{
						// �V�K�J�e�S���Ƃ��Ēǉ�
						listViewExpenseStatistics.Items.Add(new ListViewItem([category, amountText.ToString()]));
					}
				}
			}
		}

		/// <summary>
		/// �X����p���ڃy�[�W�o�^�{�^���̃N���b�N�C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonLinkStoreExpensPageRegister_Click(object sender, EventArgs e)
		{
			LinkStoreToExpenseCategory(textBox_StoreNameExpenseCategory.Text);
		}

		/// <summary>
		/// �X���^�O�\���{�^���̃N���b�N�C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonDisplayStoreNameTagList_Click(object sender, EventArgs e)
		{
			var storeNameTagLists = new StoreNameTagLists();
			storeNameTagLists.Show();
		}

		/// <summary>
		/// ��p���ړo�^���X�g�̍s���ڑI���C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ListViewExpenseRegistration_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listViewExpenseRegistration.SelectedItems.Count > 0)
			{
				var selectedItem = listViewExpenseRegistration.SelectedItems[0];
				var itemBounds = selectedItem.GetBounds(ItemBoundsPortion.Label);
				var expenseCategoryColumnWidth = transactionListView.Columns[0].Width;
				// [[comboBoxExpenseCategory]]�̕`��X�V
				comboBoxExpenseCategory.Location = new Point(itemBounds.Left + 20, itemBounds.Top + 88);
				comboBoxExpenseCategory.Width = expenseCategoryColumnWidth + 15;
				comboBoxExpenseCategory.Visible = true;
				comboBoxExpenseCategory.BringToFront();
				comboBoxExpenseCategory.Text = selectedItem.SubItems[0].Text;

				// [[textBox_StoreNameExpenseCategory]]�̕`��X�V
				if (listViewExpenseRegistration.SelectedItems.Count > 0)
				{
					var selectedStoreName = selectedItem.SubItems[1].Text;
					// �I�����ꂽ��p���ڂ��擾
					var selectedExpenseCategory = selectedItem.SubItems[0].Text;
					if (selectedExpenseCategory.Contains(" ")) selectedExpenseCategory = "���I��";
					textBox_StoreNameExpenseCategory.Text = $"{selectedStoreName}:{selectedExpenseCategory}";
				}
			}
		}

		/// <summary>
		/// ��p���ڃJ�e�S���R���{�{�b�N�X�̍��ڑI���C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ComboBoxExpenseCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			// �I������Ă��� ListView �̃A�C�e�����擾
			if (listViewExpenseRegistration.SelectedItems.Count > 0)
			{
				var selectedItem = listViewExpenseRegistration.SelectedItems[0];

				// �I�����ꂽ��p���ڂ��擾
				var selectedExpenseCategory = comboBoxExpenseCategory.SelectedItem.ToString();

				// �I�����ꂽ�s�̓X�����擾
				var selectedStoreName = selectedItem.SubItems[1].Text;

				foreach (ListViewItem item in listViewExpenseRegistration.Items)
				{
					if (item.SubItems[1].Text == selectedStoreName) // �X�����r
					{
						item.SubItems[0].Text = selectedExpenseCategory; // ��p���ڂ��X�V
					}
				}
				textBox_StoreNameExpenseCategory.Text = $"{selectedStoreName}:{selectedExpenseCategory}";
			}
		}

		/// <summary>
		/// ���X�g�^�u�̃^�u�I���C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabControlListViews_SelectedIndexChanged(object sender, EventArgs e)
		{
			comboBoxExpenseCategory.Visible = false;

		}

		Boolean isUpdatedListViewExpenseRegistrationItems = false;

		/// <summary>
		/// �X�����p���ڃJ�e�S���ƕR�Â���
		/// </summary>
		/// <param name="storeNameTag"></param>
		private void LinkStoreToExpenseCategory(String storeNameTag)
		{
			// StoreConfig�̃C���X�^���X���쐬
			StoreNameTagConfig storeConfig = new StoreNameTagConfig();

			// storeNameTag���u:�v�ŕ������āA�X���Ɣ�p���ڂ𒊏o
			var parts = storeNameTag.Split(':');
			if (parts.Length != 2)
			{
				MessageBox.Show("�����ȃt�H�[�}�b�g�ł��B�������t�H�[�}�b�g�� '�X��:��p����' �ł��B");
				return;
			}
			var storeName = parts[0];
			var expenseCategory = parts[1];
			// ���ɓ����X�����o�^����Ă��邩�m�F
			var existingTag = storeConfig.StoreNameTags
					.FirstOrDefault(tag => tag.StartsWith($"{storeName}:"));
			if (existingTag != null)
			{
				// �����̔�p���ڂ��擾
				var existingExpenseCategory = existingTag.Split(':')[1];

				// ������p���ڂ��قȂ�ꍇ�A�ύX�_�C�A���O��\��
				if (existingExpenseCategory != expenseCategory)
				{
					var result = MessageBox.Show(
							$"�X�� '{storeName}' �ɂ͊��� '{existingExpenseCategory}' ���o�^����Ă��܂��B�ύX���܂����H",
							"��p���ڂ̕ύX",
							MessageBoxButtons.YesNo
					);

					if (result == DialogResult.Yes)
					{
						// ��p���ڂ��X�V���ĕۑ�
						storeConfig.StoreNameTags.Remove(existingTag);
						storeConfig.StoreNameTags.Add(storeNameTag);
						storeConfig.Write(); // TOML�t�@�C���ɕۑ�
						var writeResult = MessageBox.Show(
							$"�X�� '{storeName}' �̔�p���ڂ� '{expenseCategory}' �ɕύX���܂����B",
							"��p���ڂ̕ύX",
							MessageBoxButtons.OK
					);
					}
				}
				else
				{
					MessageBox.Show($"{storeNameTag} �͊��ɓo�^����Ă��܂��B");
				}
			}
			else
			{
				storeConfig.StoreNameTags.Add(storeNameTag);
				storeConfig.Write(); // TOML�t�@�C���ɕۑ�
			}
		}

		/// <summary>
		/// ��p���ړo�^���X�g�̍X�V
		/// </summary>
		/// <param name="storeNameTags"></param>
		private void UpdateListViewExpenseRegistration(List<String> storeNameTags)
		{
			if (listViewExpenseRegistration.InvokeRequired)
			{
				// UI �X���b�h�ɖ߂��ď��������s
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
						var storeName = parts[0];        // �X��
						var expenseCategory = parts[1];  // ��p����

						listViewExpenseRegistration.SuspendLayout();
						// �������ڂ̍X�V�i���݂���ꍇ�j
						foreach (ListViewItem item in listViewExpenseRegistration.Items)
						{
							if (item.SubItems[1].Text == storeName) // �X������v����ꍇ
							{
								item.SubItems[0].Text = expenseCategory; // ��p���ڂ��X�V
								isUpdatedListViewExpenseRegistrationItems = true;
							}
						}
						listViewExpenseRegistration.ResumeLayout();
					}
				}

			}
		}

		/// <summary>
		/// �`��o�b�N�O���E���h���[�J�[�̒�����X�V�C�x���g
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backgroundWorkerDraw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			StoreNameTagConfig storeConfig = new StoreNameTagConfig();
			while (drawWokerRunning)
			{
				storeConfig.Load();
				var storeNameTags = storeConfig.StoreNameTags;
				if (storeNameTags != null)
				{
					UpdateListViewExpenseRegistration(storeNameTags);
				}
				Thread.Sleep(DEF_BG_SLEEP);
				if (backgroundWorkerDraw.CancellationPending)
				{
					e.Cancel = true;
					return; // �L�����Z�����ꂽ�烋�[�v�𔲂���
				}
			}
		}
	}
}
