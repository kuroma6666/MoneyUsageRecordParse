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
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            richTextBox1 = new RichTextBox();
            buttonLoadCSVFile = new Button();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            listView1.Location = new Point(14, 87);
            listView1.Name = "listView1";
            listView1.Size = new Size(620, 276);
            listView1.TabIndex = 2;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "費用項目";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(14, 428);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(620, 96);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // buttonLoadCSVFile
            // 
            buttonLoadCSVFile.Location = new Point(14, 28);
            buttonLoadCSVFile.Name = "buttonLoadCSVFile";
            buttonLoadCSVFile.Size = new Size(75, 23);
            buttonLoadCSVFile.TabIndex = 4;
            buttonLoadCSVFile.Text = "CSV読込";
            buttonLoadCSVFile.UseVisualStyleBackColor = true;
            buttonLoadCSVFile.Click += buttonLoadCSVFile_Click;
            // 
            // MoneyUsageRecordApp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(646, 536);
            Controls.Add(buttonLoadCSVFile);
            Controls.Add(richTextBox1);
            Controls.Add(listView1);
            Name = "MoneyUsageRecordApp";
            Text = "MoneyUsageRecordApp";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private RichTextBox richTextBox1;
        private Button buttonLoadCSVFile;
    }
}
