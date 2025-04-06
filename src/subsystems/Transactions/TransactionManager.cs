using SonyBankUsageRecordParse.src.subsystems.CSV;
using SonyBankUsageRecordParse.src.subsystems.CSV.Common;
using SonyBankUsageRecordParse.src.subsystems.Tagging;

using System.Diagnostics;
using System.Globalization;

namespace SonyBankUsageRecordParse.src.subsystems.Transactions
{
	public class TransactionManager
	{
		public List<UsageTransaction> Transactions { get; private set; } = new List<UsageTransaction>();

		public void ParseCSVFile(String filePath)
		{
			var parser = new CSVParser();
			Transactions = parser.ParseCSV(filePath);
		}

		public List<String> LoadStoreNameTags()
		{
			StoreNameTagConfig storeConfig = new StoreNameTagConfig();
			storeConfig.Load();
			return storeConfig.StoreNameTags;
		}

		public void LinkStoreToExpenseCategory(String storeNameTag)
		{
			StoreNameTagConfig storeConfig = new StoreNameTagConfig();
			var parts = storeNameTag.Split(':');
			if (parts.Length != 2)
			{
				throw new FormatException("無効なフォーマットです。正しいフォーマットは '店名:費用項目' です。");
			}
			var storeName = parts[0];
			var expenseCategory = parts[1];
			var existingTag = storeConfig.StoreNameTags
					.FirstOrDefault(tag => tag.StartsWith($"{storeName}:"));
			if (existingTag != null)
			{
				var existingExpenseCategory = existingTag.Split(':')[1];
				if (existingExpenseCategory != expenseCategory)
				{
					storeConfig.StoreNameTags.Remove(existingTag);
					storeConfig.StoreNameTags.Add(storeNameTag);
					storeConfig.Write();
				}
			}
			else
			{
				storeConfig.StoreNameTags.Add(storeNameTag);
				storeConfig.Write();
			}
		}
	}
}
