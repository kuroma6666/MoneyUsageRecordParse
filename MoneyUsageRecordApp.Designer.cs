namespace SonyBankUsageRecordParse
{
	partial class MoneyUsageRecordApp
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			transactionListView = new ListView();
			columnHeader_Date = new ColumnHeader();
			columnHeader_StoreName = new ColumnHeader();
			columnHeader_Amount = new ColumnHeader();
			columnHeader_Balance = new ColumnHeader();
			richTextBox1 = new RichTextBox();
			buttonParseCSVFile = new Button();
			tabControlCSV = new TabControl();
			tabTransactionsPage = new TabPage();
			tabLinkStoreExpensPage = new TabPage();
			listViewExpenseRegistration = new ListView();
			column_ExpenseCategory = new ColumnHeader();
			column_StoreName = new ColumnHeader();
			column_Amount = new ColumnHeader();
			tabExpenseCategoryStatisticsPage = new TabPage();
			listViewExpenseStatistics = new ListView();
			columnHeader_ExpenseCategory = new ColumnHeader();
			columnHeader_TotalAmount = new ColumnHeader();
			buttonLinkStoreToExpense = new Button();
			tabControlCSV.SuspendLayout();
			tabTransactionsPage.SuspendLayout();
			tabLinkStoreExpensPage.SuspendLayout();
			tabExpenseCategoryStatisticsPage.SuspendLayout();
			SuspendLayout();
			// 
			// transactionListView
			// 
			transactionListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			transactionListView.Columns.AddRange(new ColumnHeader[] { columnHeader_Date, columnHeader_StoreName, columnHeader_Amount, columnHeader_Balance });
			transactionListView.FullRowSelect = true;
			transactionListView.GridLines = true;
			transactionListView.Location = new Point(3, 6);
			transactionListView.Name = "transactionListView";
			transactionListView.Size = new Size(605, 279);
			transactionListView.TabIndex = 2;
			transactionListView.UseCompatibleStateImageBehavior = false;
			transactionListView.View = View.Details;
			// 
			// columnHeader_Date
			// 
			columnHeader_Date.Text = "日付";
			columnHeader_Date.Width = 100;
			// 
			// columnHeader_StoreName
			// 
			columnHeader_StoreName.Text = "店名";
			columnHeader_StoreName.Width = 200;
			// 
			// columnHeader_Amount
			// 
			columnHeader_Amount.Text = "支払金額";
			columnHeader_Amount.Width = 100;
			// 
			// columnHeader_Balance
			// 
			columnHeader_Balance.Text = "残額";
			columnHeader_Balance.Width = 100;
			// 
			// richTextBox1
			// 
			richTextBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			richTextBox1.Location = new Point(14, 428);
			richTextBox1.Name = "richTextBox1";
			richTextBox1.Size = new Size(620, 96);
			richTextBox1.TabIndex = 3;
			richTextBox1.Text = "";
			// 
			// buttonParseCSVFile
			// 
			buttonParseCSVFile.Location = new Point(14, 28);
			buttonParseCSVFile.Name = "buttonParseCSVFile";
			buttonParseCSVFile.Size = new Size(116, 23);
			buttonParseCSVFile.TabIndex = 4;
			buttonParseCSVFile.Text = "Exec CSV Parse";
			buttonParseCSVFile.UseVisualStyleBackColor = true;
			buttonParseCSVFile.Click += buttoParseCSVFile_Click;
			// 
			// tabControlCSV
			// 
			tabControlCSV.Controls.Add(tabTransactionsPage);
			tabControlCSV.Controls.Add(tabLinkStoreExpensPage);
			tabControlCSV.Controls.Add(tabExpenseCategoryStatisticsPage);
			tabControlCSV.Font = new Font("Meiryo UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
			tabControlCSV.Location = new Point(12, 58);
			tabControlCSV.Name = "tabControlCSV";
			tabControlCSV.SelectedIndex = 0;
			tabControlCSV.Size = new Size(622, 321);
			tabControlCSV.TabIndex = 5;
			// 
			// tabTransactionsPage
			// 
			tabTransactionsPage.Controls.Add(transactionListView);
			tabTransactionsPage.Location = new Point(4, 26);
			tabTransactionsPage.Name = "tabTransactionsPage";
			tabTransactionsPage.Padding = new Padding(3);
			tabTransactionsPage.Size = new Size(614, 291);
			tabTransactionsPage.TabIndex = 0;
			tabTransactionsPage.Text = "利用明細";
			tabTransactionsPage.UseVisualStyleBackColor = true;
			// 
			// tabLinkStoreExpensPage
			// 
			tabLinkStoreExpensPage.Controls.Add(listViewExpenseRegistration);
			tabLinkStoreExpensPage.Location = new Point(4, 26);
			tabLinkStoreExpensPage.Name = "tabLinkStoreExpensPage";
			tabLinkStoreExpensPage.Padding = new Padding(3);
			tabLinkStoreExpensPage.Size = new Size(614, 291);
			tabLinkStoreExpensPage.TabIndex = 1;
			tabLinkStoreExpensPage.Text = "費用項目登録";
			tabLinkStoreExpensPage.UseVisualStyleBackColor = true;
			// 
			// listViewExpenseRegistration
			// 
			listViewExpenseRegistration.Columns.AddRange(new ColumnHeader[] { column_ExpenseCategory, column_StoreName, column_Amount });
			listViewExpenseRegistration.FullRowSelect = true;
			listViewExpenseRegistration.GridLines = true;
			listViewExpenseRegistration.Location = new Point(3, 6);
			listViewExpenseRegistration.Name = "listViewExpenseRegistration";
			listViewExpenseRegistration.Size = new Size(605, 281);
			listViewExpenseRegistration.TabIndex = 0;
			listViewExpenseRegistration.UseCompatibleStateImageBehavior = false;
			listViewExpenseRegistration.View = View.Details;
			listViewExpenseRegistration.SelectedIndexChanged += listViewExpenseRegistration_SelectedIndexChanged;
			// 
			// column_ExpenseCategory
			// 
			column_ExpenseCategory.Text = "費用項目";
			column_ExpenseCategory.Width = 120;
			// 
			// column_StoreName
			// 
			column_StoreName.Text = "店名";
			column_StoreName.Width = 200;
			// 
			// column_Amount
			// 
			column_Amount.Text = "金額";
			column_Amount.Width = 120;
			// 
			// tabExpenseCategoryStatisticsPage
			// 
			tabExpenseCategoryStatisticsPage.Controls.Add(listViewExpenseStatistics);
			tabExpenseCategoryStatisticsPage.Location = new Point(4, 26);
			tabExpenseCategoryStatisticsPage.Name = "tabExpenseCategoryStatisticsPage";
			tabExpenseCategoryStatisticsPage.Padding = new Padding(3);
			tabExpenseCategoryStatisticsPage.Size = new Size(614, 291);
			tabExpenseCategoryStatisticsPage.TabIndex = 2;
			tabExpenseCategoryStatisticsPage.Text = "費用項目統計";
			tabExpenseCategoryStatisticsPage.UseVisualStyleBackColor = true;
			// 
			// listViewExpenseStatistics
			// 
			listViewExpenseStatistics.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			listViewExpenseStatistics.Columns.AddRange(new ColumnHeader[] { columnHeader_ExpenseCategory, columnHeader_TotalAmount });
			listViewExpenseStatistics.FullRowSelect = true;
			listViewExpenseStatistics.GridLines = true;
			listViewExpenseStatistics.Location = new Point(3, 6);
			listViewExpenseStatistics.Name = "listViewExpenseStatistics";
			listViewExpenseStatistics.Size = new Size(605, 281);
			listViewExpenseStatistics.TabIndex = 0;
			listViewExpenseStatistics.UseCompatibleStateImageBehavior = false;
			listViewExpenseStatistics.View = View.Details;
			// 
			// columnHeader_ExpenseCategory
			// 
			columnHeader_ExpenseCategory.Text = "費用項目";
			columnHeader_ExpenseCategory.Width = 120;
			// 
			// columnHeader_TotalAmount
			// 
			columnHeader_TotalAmount.Text = "合計金額";
			columnHeader_TotalAmount.Width = 120;
			// 
			// buttonLinkStoreToExpense
			// 
			buttonLinkStoreToExpense.Location = new Point(136, 28);
			buttonLinkStoreToExpense.Name = "buttonLinkStoreToExpense";
			buttonLinkStoreToExpense.Size = new Size(152, 23);
			buttonLinkStoreToExpense.TabIndex = 6;
			buttonLinkStoreToExpense.Text = "Exec LinkStoreExpense";
			buttonLinkStoreToExpense.UseVisualStyleBackColor = true;
			buttonLinkStoreToExpense.Click += buttonLinkStoreToExpense_Click;
			// 
			// MoneyUsageRecordApp
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(646, 536);
			Controls.Add(buttonLinkStoreToExpense);
			Controls.Add(tabControlCSV);
			Controls.Add(buttonParseCSVFile);
			Controls.Add(richTextBox1);
			Font = new Font("Meiryo UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
			Name = "MoneyUsageRecordApp";
			Text = "MoneyUsageRecordApp";
			Load += Form1_Load;
			tabControlCSV.ResumeLayout(false);
			tabTransactionsPage.ResumeLayout(false);
			tabLinkStoreExpensPage.ResumeLayout(false);
			tabExpenseCategoryStatisticsPage.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion
		private ListView transactionListView;
		private ColumnHeader columnHeader_Date;
		private RichTextBox richTextBox1;
		private Button buttonParseCSVFile;
		private ColumnHeader columnHeader_StoreName;
		private ColumnHeader columnHeader_Amount;
		private ColumnHeader columnHeader_Balance;
		private TabControl tabControlCSV;
		private TabPage tabTransactionsPage;
		private TabPage tabLinkStoreExpensPage;
		private Button buttonLinkStoreToExpense;
		private TabPage tabExpenseCategoryStatisticsPage;
		private ListView listViewExpenseRegistration;
		private ColumnHeader column_StoreName;
		private ColumnHeader column_Amount;
		private ColumnHeader column_ExpenseCategory;
		private ListView listViewExpenseStatistics;
		private ColumnHeader columnHeader_ExpenseCategory;
		private ColumnHeader columnHeader_TotalAmount;
	}
}
