using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

using SonyBankUsageRecordParse.src.subsystems.CSV.Common;

using SonyBankUsageRecordParse.src.subsystems.Transactions;

namespace SonyBankUsageRecordParse.src.subsystems.CSV.ICSVFormat
{
	public class SMBCCSVParser : ICSVFormatParser
	{
		public List<UsageTransaction> Parse(StreamReader reader)
		{
			var transactions = new List<UsageTransaction>();
			String line;

			reader.ReadLine();

			while ((line = reader.ReadLine()) != null)
			{
				if (String.IsNullOrWhiteSpace(line)) continue;

				var values = CSVUtilitiy.ParseCsvLine(line);

				if (values.Length < 5)
				{
					Debug.WriteLine($"不正な行が検出されました: {line}");
					continue;
				}

				try
				{
					var date = DateTime.ParseExact(values[0].Trim(), "yyyy/MM/dd", CultureInfo.InvariantCulture);
					var storeName = values[1].Trim();
					var amount = Decimal.Parse(values[2].Trim(), NumberStyles.AllowThousands, CultureInfo.InvariantCulture);

					var balance = 0;

					transactions.Add(new UsageTransaction(date, storeName, amount, balance));
				}
				catch (FormatException ex)
				{
					Debug.WriteLine($"データの形式に問題があります: {ex.Message}");
				}
			}

			return transactions;
		}
	}
}