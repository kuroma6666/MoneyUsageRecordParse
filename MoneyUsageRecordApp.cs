using SonyBankUsageRecordParse.src.subsystems.LoadCSV;
using System.Diagnostics;

namespace SonyBankUsageRecordParse
{
  public partial class MoneyUsageRecordApp : Form
  {
    public MoneyUsageRecordApp()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void buttoParseCSVFile_Click(object sender, EventArgs e)
    {
      String csvFilePath = SelectCSVFile();
      if(csvFilePath != null)
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
			}
		}
    private static String SelectCSVFile()
    {
      using OpenFileDialog openFileDialog = new OpenFileDialog
      {
        Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
        Title = "CSVファイルを選択してください"
      };
      return openFileDialog.ShowDialog() == DialogResult.OK ? openFileDialog.FileName : null;
    }
  }
}
