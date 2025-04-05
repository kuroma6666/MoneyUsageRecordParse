using System.Diagnostics;
using System.Globalization;
using System.Text;

using SonyBankUsageRecordParse.src.subsystems.Transactions;
using SonyBankUsageRecordParse.src.subsystems.CSV.Common;

namespace SonyBankUsageRecordParse.src.subsystems.CSV
{
	public class CSVParser
	{
		public List<UsageTransaction> ParseCSV(String filePath)
		{
			var transactions = new List<UsageTransaction>();

			try
			{
				Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
				using (var reader = new StreamReader(filePath, Encoding.GetEncoding("Shift-JIS")))
				{
					String line;
					Boolean isFirstLine = true;

					while ((line = reader.ReadLine()) != null)
					{
						if (isFirstLine)
						{
							isFirstLine = false;
							continue;
						}

						if (String.IsNullOrWhiteSpace(line)) continue;
						if (!line.Contains("Visaデビット")) continue;

						var values = CSVUtilitiy.ParseCsvLine(line);

						if (values.Length < 6)
						{
							Debug.WriteLine($"不正な行が検出されました: {line}");
							continue;
						}

						try
						{
							var date = DateTime.ParseExact(values[0].Trim('"'), "yyyy年MM月dd日", CultureInfo.InvariantCulture);
							var description = values[1].Trim('"');
							var extractionTag = "Visaデビット";
							var storeName = CSVUtilitiy.ExtractStoreName(description, extractionTag);

							var amountString = values[4];
							Decimal amount = CSVUtilitiy.ParseAmount(amountString);

							var balanceString = values[5].Replace(",", "").Trim('"');
							Decimal balance = String.IsNullOrWhiteSpace(balanceString) ? 0 : Decimal.Parse(balanceString);

							transactions.Add(new UsageTransaction(date, storeName, amount, balance));
						}
						catch (FormatException ex)
						{
							Console.WriteLine($"データの形式に問題があります: {ex.Message}");
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"エラーが発生しました: {ex.Message}");
			}

			return transactions;
		}
	}
}
