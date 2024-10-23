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
			richTextBox_DispLog = new RichTextBox();
			buttonParseCSVFile = new Button();
			tabControlListViews = new TabControl();
			tabTransactionsPage = new TabPage();
			tabLinkStoreExpensPage = new TabPage();
			groupBox_LinkStoreExpensPageCtrl = new GroupBox();
			buttonLinkStoreExpensPageRegister = new Button();
			textBox_StoreNameExpenseCategory = new TextBox();
			tabExpenseCategoryStatisticsPage = new TabPage();
			listViewExpenseStatistics = new ListView();
			columnHeader_ExpenseCategory = new ColumnHeader();
			columnHeader_TotalAmount = new ColumnHeader();
			buttonGenerateStatistics = new Button();
			buttonDisplayStoreNameTagList = new Button();
			backgroundWorkerDraw = new System.ComponentModel.BackgroundWorker();
			listViewExpenseRegistration = new ListView();
			column_ExpenseCategory = new ColumnHeader();
			column_StoreName = new ColumnHeader();
			column_Amount = new ColumnHeader();
			tabControlListViews.SuspendLayout();
			tabTransactionsPage.SuspendLayout();
			tabLinkStoreExpensPage.SuspendLayout();
			groupBox_LinkStoreExpensPageCtrl.SuspendLayout();
			tabExpenseCategoryStatisticsPage.SuspendLayout();
			SuspendLayout();
			// 
			// transactionListView
			// 
			transactionListView.Columns.AddRange(new ColumnHeader[] { columnHeader_Date, columnHeader_StoreName, columnHeader_Amount, columnHeader_Balance });
			transactionListView.Dock = DockStyle.Fill;
			transactionListView.FullRowSelect = true;
			transactionListView.GridLines = true;
			transactionListView.Location = new Point(3, 3);
			transactionListView.Name = "transactionListView";
			transactionListView.Size = new Size(646, 467);
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
			// richTextBox_DispLog
			// 
			richTextBox_DispLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			richTextBox_DispLog.Location = new Point(14, 588);
			richTextBox_DispLog.Name = "richTextBox_DispLog";
			richTextBox_DispLog.Size = new Size(658, 96);
			richTextBox_DispLog.TabIndex = 3;
			richTextBox_DispLog.Text = "";
			// 
			// buttonParseCSVFile
			// 
			buttonParseCSVFile.Location = new Point(14, 28);
			buttonParseCSVFile.Name = "buttonParseCSVFile";
			buttonParseCSVFile.Size = new Size(116, 23);
			buttonParseCSVFile.TabIndex = 4;
			buttonParseCSVFile.Text = "Exec CSV Parse";
			buttonParseCSVFile.UseVisualStyleBackColor = true;
			buttonParseCSVFile.Click += ButtoParseCSVFile_Click;
			// 
			// tabControlListViews
			// 
			tabControlListViews.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			tabControlListViews.Controls.Add(tabTransactionsPage);
			tabControlListViews.Controls.Add(tabLinkStoreExpensPage);
			tabControlListViews.Controls.Add(tabExpenseCategoryStatisticsPage);
			tabControlListViews.Font = new Font("Meiryo UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
			tabControlListViews.Location = new Point(12, 58);
			tabControlListViews.Name = "tabControlListViews";
			tabControlListViews.SelectedIndex = 0;
			tabControlListViews.Size = new Size(660, 503);
			tabControlListViews.TabIndex = 5;
			tabControlListViews.SelectedIndexChanged += TabControlListViews_SelectedIndexChanged;
			// 
			// tabTransactionsPage
			// 
			tabTransactionsPage.Controls.Add(transactionListView);
			tabTransactionsPage.Location = new Point(4, 26);
			tabTransactionsPage.Name = "tabTransactionsPage";
			tabTransactionsPage.Padding = new Padding(3);
			tabTransactionsPage.Size = new Size(652, 473);
			tabTransactionsPage.TabIndex = 0;
			tabTransactionsPage.Text = "利用明細";
			tabTransactionsPage.UseVisualStyleBackColor = true;
			// 
			// tabLinkStoreExpensPage
			// 
			tabLinkStoreExpensPage.Controls.Add(listViewExpenseRegistration);
			tabLinkStoreExpensPage.Controls.Add(groupBox_LinkStoreExpensPageCtrl);
			tabLinkStoreExpensPage.Location = new Point(4, 26);
			tabLinkStoreExpensPage.Name = "tabLinkStoreExpensPage";
			tabLinkStoreExpensPage.Padding = new Padding(3);
			tabLinkStoreExpensPage.Size = new Size(652, 473);
			tabLinkStoreExpensPage.TabIndex = 1;
			tabLinkStoreExpensPage.Text = "費用項目登録";
			tabLinkStoreExpensPage.UseVisualStyleBackColor = true;
			// 
			// groupBox_LinkStoreExpensPageCtrl
			// 
			groupBox_LinkStoreExpensPageCtrl.Anchor = AnchorStyles.Bottom;
			groupBox_LinkStoreExpensPageCtrl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			groupBox_LinkStoreExpensPageCtrl.Controls.Add(buttonLinkStoreExpensPageRegister);
			groupBox_LinkStoreExpensPageCtrl.Controls.Add(textBox_StoreNameExpenseCategory);
			groupBox_LinkStoreExpensPageCtrl.Location = new Point(3, 391);
			groupBox_LinkStoreExpensPageCtrl.Name = "groupBox_LinkStoreExpensPageCtrl";
			groupBox_LinkStoreExpensPageCtrl.Size = new Size(643, 76);
			groupBox_LinkStoreExpensPageCtrl.TabIndex = 2;
			groupBox_LinkStoreExpensPageCtrl.TabStop = false;
			groupBox_LinkStoreExpensPageCtrl.Text = "操作エリア";
			// 
			// buttonLinkStoreExpensPageRegister
			// 
			buttonLinkStoreExpensPageRegister.Location = new Point(6, 52);
			buttonLinkStoreExpensPageRegister.Name = "buttonLinkStoreExpensPageRegister";
			buttonLinkStoreExpensPageRegister.Size = new Size(197, 23);
			buttonLinkStoreExpensPageRegister.TabIndex = 3;
			buttonLinkStoreExpensPageRegister.Text = "タグ登録";
			buttonLinkStoreExpensPageRegister.UseVisualStyleBackColor = true;
			buttonLinkStoreExpensPageRegister.Click += buttonLinkStoreExpensPageRegister_Click;
			// 
			// textBox_StoreNameExpenseCategory
			// 
			textBox_StoreNameExpenseCategory.AcceptsReturn = true;
			textBox_StoreNameExpenseCategory.Location = new Point(6, 22);
			textBox_StoreNameExpenseCategory.Name = "textBox_StoreNameExpenseCategory";
			textBox_StoreNameExpenseCategory.Size = new Size(197, 24);
			textBox_StoreNameExpenseCategory.TabIndex = 2;
			// 
			// tabExpenseCategoryStatisticsPage
			// 
			tabExpenseCategoryStatisticsPage.Controls.Add(listViewExpenseStatistics);
			tabExpenseCategoryStatisticsPage.Location = new Point(4, 26);
			tabExpenseCategoryStatisticsPage.Name = "tabExpenseCategoryStatisticsPage";
			tabExpenseCategoryStatisticsPage.Padding = new Padding(3);
			tabExpenseCategoryStatisticsPage.Size = new Size(652, 473);
			tabExpenseCategoryStatisticsPage.TabIndex = 2;
			tabExpenseCategoryStatisticsPage.Text = "費用項目統計";
			tabExpenseCategoryStatisticsPage.UseVisualStyleBackColor = true;
			// 
			// listViewExpenseStatistics
			// 
			listViewExpenseStatistics.Columns.AddRange(new ColumnHeader[] { columnHeader_ExpenseCategory, columnHeader_TotalAmount });
			listViewExpenseStatistics.Dock = DockStyle.Fill;
			listViewExpenseStatistics.FullRowSelect = true;
			listViewExpenseStatistics.GridLines = true;
			listViewExpenseStatistics.Location = new Point(3, 3);
			listViewExpenseStatistics.Name = "listViewExpenseStatistics";
			listViewExpenseStatistics.Size = new Size(646, 467);
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
			// buttonGenerateStatistics
			// 
			buttonGenerateStatistics.Location = new Point(136, 28);
			buttonGenerateStatistics.Name = "buttonGenerateStatistics";
			buttonGenerateStatistics.Size = new Size(169, 23);
			buttonGenerateStatistics.TabIndex = 6;
			buttonGenerateStatistics.Text = "Exec GenerateStatistics";
			buttonGenerateStatistics.UseVisualStyleBackColor = true;
			buttonGenerateStatistics.Click += ButtonGenerateStatistics_Click;
			// 
			// buttonDisplayStoreNameTagList
			// 
			buttonDisplayStoreNameTagList.Location = new Point(311, 28);
			buttonDisplayStoreNameTagList.Name = "buttonDisplayStoreNameTagList";
			buttonDisplayStoreNameTagList.Size = new Size(202, 23);
			buttonDisplayStoreNameTagList.TabIndex = 6;
			buttonDisplayStoreNameTagList.Text = "Exec DisplayStoreNameTags";
			buttonDisplayStoreNameTagList.UseVisualStyleBackColor = true;
			buttonDisplayStoreNameTagList.Click += ButtonDisplayStoreNameTagList_Click;
			// 
			// backgroundWorkerDraw
			// 
			backgroundWorkerDraw.WorkerSupportsCancellation = true;
			backgroundWorkerDraw.DoWork += backgroundWorkerDraw_DoWork;
			// 
			// listViewExpenseRegistration
			// 
			listViewExpenseRegistration.Columns.AddRange(new ColumnHeader[] { column_ExpenseCategory, column_StoreName, column_Amount });
			listViewExpenseRegistration.FullRowSelect = true;
			listViewExpenseRegistration.GridLines = true;
			listViewExpenseRegistration.Location = new Point(3, 3);
			listViewExpenseRegistration.Name = "listViewExpenseRegistration";
			listViewExpenseRegistration.Size = new Size(629, 382);
			listViewExpenseRegistration.TabIndex = 0;
			listViewExpenseRegistration.UseCompatibleStateImageBehavior = false;
			listViewExpenseRegistration.View = View.Details;
			listViewExpenseRegistration.SelectedIndexChanged += ListViewExpenseRegistration_SelectedIndexChanged;
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
			// MoneyUsageRecordApp
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(684, 696);
			Controls.Add(buttonDisplayStoreNameTagList);
			Controls.Add(buttonGenerateStatistics);
			Controls.Add(tabControlListViews);
			Controls.Add(buttonParseCSVFile);
			Controls.Add(richTextBox_DispLog);
			Font = new Font("Meiryo UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
			Name = "MoneyUsageRecordApp";
			Text = "MoneyUsageRecordApp";
			Load += MoneyUsagerRecordApp_Load;
			tabControlListViews.ResumeLayout(false);
			tabTransactionsPage.ResumeLayout(false);
			tabLinkStoreExpensPage.ResumeLayout(false);
			groupBox_LinkStoreExpensPageCtrl.ResumeLayout(false);
			groupBox_LinkStoreExpensPageCtrl.PerformLayout();
			tabExpenseCategoryStatisticsPage.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion
		private ListView transactionListView;
		private ColumnHeader columnHeader_Date;
		private RichTextBox richTextBox_DispLog;
		private Button buttonParseCSVFile;
		private ColumnHeader columnHeader_StoreName;
		private ColumnHeader columnHeader_Amount;
		private ColumnHeader columnHeader_Balance;
		private TabControl tabControlListViews;
		private TabPage tabTransactionsPage;
		private TabPage tabLinkStoreExpensPage;
		private Button buttonGenerateStatistics;
		private TabPage tabExpenseCategoryStatisticsPage;
		private ListView listViewExpenseStatistics;
		private ColumnHeader columnHeader_ExpenseCategory;
		private ColumnHeader columnHeader_TotalAmount;
		private GroupBox groupBox_LinkStoreExpensPageCtrl;
		private Button buttonLinkStoreExpensPageRegister;
		private TextBox textBox_StoreNameExpenseCategory;
		private Button buttonDisplayStoreNameTagList;
		private System.ComponentModel.BackgroundWorker backgroundWorkerDraw;
		private ListView listViewExpenseRegistration;
		private ColumnHeader column_ExpenseCategory;
		private ColumnHeader column_StoreName;
		private ColumnHeader column_Amount;
	}
}
