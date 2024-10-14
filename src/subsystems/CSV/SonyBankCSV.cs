using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using SonyBankUsageRecordParse.src.subsystems.CSV.Common;

namespace SonyBankUsageRecordParse.src.subsystems.CSV
{
	public class SonyBankCSV
	{
		public class UsageTransaction
		{
			public DateTime Date { get; set; }
			public String StoreName { get; set; }
			public Decimal Amount { get; set; }
			public Decimal Balance { get; set; }
			public override String ToString()
			{
				return $"{Date.ToString("yyyy-MM-dd")} | {StoreName} | {Amount} | {Balance}";
			}
		}

		public List<UsageTransaction> ParseCSV(String filePath)
		{
			var transactions = new List<UsageTransaction>();

			try
			{
				// Shift-JISエンコーディングをサポートするプロバイダーを登録
				Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
				// Shift-JISエンコーディングを指定してファイルを読み込む
				using (var reader = new StreamReader(filePath, Encoding.GetEncoding("Shift-JIS")))
				{
					String line;
					Boolean isFirstLine = true;

					while ((line = reader.ReadLine()) != null)
					{
						// ヘッダー行をスキップ
						if (isFirstLine)
						{
							isFirstLine = false;
							continue;
						}

						// 空白行をスキップ
						if (String.IsNullOrWhiteSpace(line)) continue;
						// 「Visaデビット」の明細以外スキップ
						if (!line.Contains("Visaデビット")) continue;

						var values = ParseCsvLine(line);

						// 列数チェック（少なくとも6列必要）
						if (values.Length < 6)
						{
							Debug.WriteLine($"不正な行が検出されました: {line}");
							continue;
						}

						try
						{
							// 日付、説明、金額、残高をパース
							var date = DateTime.ParseExact(values[0].Trim('"'), "yyyy年MM月dd日", CultureInfo.InvariantCulture);
							var description = values[1].Trim('"');
							var extractionTag = "Visaデビット";
							var storeName = CSVUtilitiy.ExtractStoreName(description, extractionTag);

							// 金額のパース
							var amountString = values[4];
							Decimal amount = ParseAmount(amountString);

							// 残額のパース
							var balanceString = values[5].Replace(",", "").Trim('"');
							Decimal balance = String.IsNullOrWhiteSpace(balanceString) ? 0 : Decimal.Parse(balanceString);

							// [[UsageTransaction]]レコードを追加
							transactions.Add(new UsageTransaction
							{
								Date = date,
								StoreName = storeName,
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


		private static String[] ParseCsvLine(String line)
		{
			// CSVの行を正しく分割する
			var values = new List<String>();
			var currentValue = new StringBuilder();
			Boolean insideQuotes = false;

			foreach (var c in line)
			{
				if (c == '"' && (currentValue.Length == 0 || currentValue[currentValue.Length - 1] != '\\'))
				{
					insideQuotes = !insideQuotes; // 引用符の内外を切り替え
				}
				else if (c == ',' && !insideQuotes)
				{
					// 引用符の外でカンマに遭遇した場合、値をリストに追加
					values.Add(currentValue.ToString().Trim('"')); // トリムして追加
					currentValue.Clear(); // 現在の値をクリア
				}
				else
				{
					currentValue.Append(c); // 現在の値に文字を追加
				}
			}

			// 最後の値を追加
			if (currentValue.Length > 0)
			{
				values.Add(currentValue.ToString().Trim('"'));
			}

			return values.ToArray();
		}

		private static Decimal ParseAmount(String amountString)
		{
			amountString = amountString.Replace(",", "").Trim(); // カンマを取り除き、余分な空白を削除
			Decimal amount;

			if (String.IsNullOrWhiteSpace(amountString))
			{
				return 0; // 空または空白の場合は0
			}
			else if (!Decimal.TryParse(amountString, out amount))
			{
				throw new FormatException($"'{amountString}' は有効な数値ではありません。"); // 変換失敗の場合は例外をスロー
			}

			return amount; // パースした数値を返す
		}
	}


}
