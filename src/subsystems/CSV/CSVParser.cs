using System.Diagnostics;
using System.Globalization;
using System.Text;

using SonyBankUsageRecordParse.src.subsystems.Transactions;
using SonyBankUsageRecordParse.src.subsystems.CSV.Common;

namespace SonyBankUsageRecordParse.src.subsystems.CSV
{
	public class CSVParser
	{
		private readonly ICSVFormatParser _formatParser;

		public CSVParser(ICSVFormatParser formatParser)
		{
			_formatParser = formatParser;
		}

		public List<UsageTransaction> ParseCSV(String filePath)
		{
			var transactions = new List<UsageTransaction>();

			try
			{
				Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
				using (var reader = new StreamReader(filePath, Encoding.GetEncoding("Shift-JIS")))
				{
					transactions = _formatParser.Parse(reader);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"ÉGÉâÅ[Ç™î≠ê∂ÇµÇ‹ÇµÇΩ: {ex.Message}");
			}

			return transactions;
		}
	}
}
