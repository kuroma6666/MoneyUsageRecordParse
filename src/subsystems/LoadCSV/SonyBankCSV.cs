using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SonyBankUsageRecordParse.src.subsystems.LoadCSV
{
	public class SonyBankCSV
	{
		public class Transaction
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
							var storeName = ExtractStoreName(description);

							// 金額のパース
							var amountString = values[4].Replace(",", "").Trim('"');
							Decimal amount = String.IsNullOrWhiteSpace(amountString) ? 0 : Decimal.Parse(amountString);

							// 残額のパース
							var balanceString = values[5].Replace(",", "").Trim('"');
							Decimal balance = String.IsNullOrWhiteSpace(balanceString) ? 0 : Decimal.Parse(balanceString);


							transactions.Add(new Transaction
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

		private String ExtractStoreName(String description)
		{
			const String UsageTypeTag = "Visaデビット";
			// 店名を抽出するための簡単な処理
			if (description.Contains(UsageTypeTag))
			{
				// "Visaデビット"の後の部分を取得
				var parts = description.Split(new[] { ' ', '　' }, StringSplitOptions.RemoveEmptyEntries);
				// "Visaデビット"の次からすべてを結合して店名とする
				Int32 startIndex = Array.IndexOf(parts, UsageTypeTag) + 1;
				if (startIndex < parts.Length)
				{
					// 数字以外の部分をフィルタリング
					var storeParts = new List<string>();
					for (int i = startIndex; i < parts.Length; i++)
					{
						if (!Regex.IsMatch(parts[i], @"^\d+$")) // 数字のみの部分を除外
						{
							storeParts.Add(parts[i]);
						}
					}
					return string.Join(" ", storeParts); // 店名を結合して返す
				}
			}
			return String.Empty;
		}
	}
}
