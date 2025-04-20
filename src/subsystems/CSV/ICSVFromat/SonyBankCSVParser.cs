using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

using SonyBankUsageRecordParse.src.subsystems.CSV.Common;

using SonyBankUsageRecordParse.src.subsystems.Transactions;

namespace SonyBankUsageRecordParse.src.subsystems.CSV.ICSVFormat
{
	public class SonyBankCSVParser : ICSVFormatParser
	{
		public List<UsageTransaction> Parse(StreamReader reader)
		{
			var transactions = new List<UsageTransaction>();
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

			return transactions;
		}
	}
}