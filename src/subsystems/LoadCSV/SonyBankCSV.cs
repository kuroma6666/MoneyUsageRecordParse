using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonyBankUsageRecordParse.src.subsystems.LoadCSV
{
    public class SonyBankCSV
    {
        public class Transaction
        {
            public DateTime Date { get; set; }
            public String Description { get; set; }
            public Decimal Amount { get; set; }
            public Decimal Balance { get; set; }

            public override String ToString()
            {
                return $"{Date.ToString("yyyy-MM-dd")} | {Description} | {Amount} | {Balance}";
            }
        }

    public List<Transaction> ParseCSV(String filePath)
    {
			var transactions = new List<Transaction>();

			try
			{
				// Shift-JISエンコーディングをサポートするプロバイダーを登録
				Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
				// Shift-JISエンコーディングを指定してファイルを読み込む
				using (var reader = new StreamReader(filePath, Encoding.GetEncoding("Shift-JIS")))
				{
					string line;
					bool isFirstLine = true;

					while ((line = reader.ReadLine()) != null)
					{
						// ヘッダー行をスキップ
						if (isFirstLine)
						{
							isFirstLine = false;
							continue;
						}

						// 空白行をスキップ
						if (string.IsNullOrWhiteSpace(line)) continue;

						var values = line.Split(',');

						// 列数チェック（少なくとも6列必要）
						if (values.Length < 6)
						{
							Console.WriteLine($"不正な行が検出されました: {line}");
							continue;
						}

						try
						{
							// 日付、説明、金額、残高をパース
							var date = DateTime.ParseExact(values[0].Trim('"'), "yyyy年MM月dd日", CultureInfo.InvariantCulture);
							var description = values[1].Trim('"');

							// 金額のパース
							var amountString = values[4].Replace(",", "").Trim('"');
							decimal amount = string.IsNullOrWhiteSpace(amountString) ? 0 : decimal.Parse(amountString);

							// 残額のパース
							var balanceString = values[5].Replace(",", "").Trim('"');
							decimal balance = string.IsNullOrWhiteSpace(balanceString) ? 0 : decimal.Parse(balanceString);


							transactions.Add(new Transaction
							{
								Date = date,
								Description = description,
								Amount = amount,
								Balance = balance
							});
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
