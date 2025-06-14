using System.Text.Json;

namespace SonyBankUsageRecordParse.src.subsystems.Expense
{
	public class JsonExpenseCategoryProvider : IExpenseCategoryProvider
	{
		private readonly string _filePath;

		public JsonExpenseCategoryProvider(string filePath)
		{
			_filePath = filePath;
		}

		public List<string> LoadCategories()
		{
			if (!File.Exists(_filePath))
			{
				return new List<string>(); // ファイルが存在しない場合は空のリストを返す
			}

			var json = File.ReadAllText(_filePath);
			return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
		}

		public void SaveCategories(List<string> categories)
		{
			var json = JsonSerializer.Serialize(categories, new JsonSerializerOptions { WriteIndented = true });
			File.WriteAllText(_filePath, json);
		}
	}
}
