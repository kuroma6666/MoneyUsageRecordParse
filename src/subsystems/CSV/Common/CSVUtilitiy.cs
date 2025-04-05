using System.Text;
using System.Text.RegularExpressions;

namespace SonyBankUsageRecordParse.src.subsystems.CSV.Common
{
	public class CSVUtilitiy
	{
		public static String ExtractStoreName(String description, String usageTypeTag)
		{
			// 店名を抽出するための簡単な処理
			if (description.Contains(usageTypeTag))
			{
				// [[usageTypeTag]]の後の部分を取得
				var parts = description.Split(new[] { ' ', '　' }, StringSplitOptions.RemoveEmptyEntries);

				// [[usageTypeTag]]の次からすべてを結合して店名とする
				Int32 startIndex = Array.IndexOf(parts, usageTypeTag) + 1;
				if (startIndex < parts.Length)
				{
					// 数字以外の部分をフィルタリング
					var storeParts = new List<String>();
					for (int i = startIndex; i < parts.Length; i++)
					{
						if (!Regex.IsMatch(parts[i], @"^\d+$")) // 数字のみの部分を除外
						{
							storeParts.Add(parts[i]);
						}
					}
					return String.Join(" ", storeParts); // 店名を結合して返す
				}
			}
			return String.Empty;
		}

		public static String SelectCSVFile()
		{
			using OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
				Title = "CSVファイルを選択してください"
			};
			return openFileDialog.ShowDialog() == DialogResult.OK ? openFileDialog.FileName : null;
		}

		public static String[] ParseCsvLine(String line)
		{
			var values = new List<String>();
			var currentValue = new StringBuilder();
			Boolean insideQuotes = false;

			foreach (var c in line)
			{
				if (c == '"' && (currentValue.Length == 0 || currentValue[currentValue.Length - 1] != '\\'))
				{
					insideQuotes = !insideQuotes;
				}
				else if (c == ',' && !insideQuotes)
				{
					values.Add(currentValue.ToString().Trim('"'));
					currentValue.Clear();
				}
				else
				{
					currentValue.Append(c);
				}
			}

			if (currentValue.Length > 0)
			{
				values.Add(currentValue.ToString().Trim('"'));
			}

			return values.ToArray();
		}

		public static Decimal ParseAmount(String amountString)
		{
			amountString = amountString.Replace(",", "").Trim();
			Decimal amount;

			if (String.IsNullOrWhiteSpace(amountString))
			{
				return 0;
			}
			else if (!Decimal.TryParse(amountString, out amount))
			{
				throw new FormatException($"'{amountString}' は有効な数値ではありません。");
			}

			return amount;
		}
	}
}
