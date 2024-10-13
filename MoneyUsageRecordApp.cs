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
			}
		}
    private static String SelectCSVFile()
    {
      using OpenFileDialog openFileDialog = new OpenFileDialog
      {
        Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
        Title = "CSV�t�@�C����I�����Ă�������"
      };
      return openFileDialog.ShowDialog() == DialogResult.OK ? openFileDialog.FileName : null;
    }
  }
}
