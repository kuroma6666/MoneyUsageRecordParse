using Nett;


namespace SonyBankUsageRecordParse.src.subsystems.Tagging
{
	public class StoreNameTagConfig
	{
		private const String TomlFilePath = "storeNameTagConfig.toml";
		public List<String> StoreNameTags { get; private set; } = [];

		public StoreNameTagConfig()
		{
			Load();

		}
		public void Load()
		{
			if (System.IO.File.Exists(TomlFilePath))
			{
				var table = Toml.ReadFile(TomlFilePath);
				if (table.TryGetValue("StoreNameTag", out TomlObject tags))
				{
					// 値がTomlArray型かを確認し、キャストする
					if (tags is TomlArray tagsArray)
					{
						// TomlArrayをリストに変換
						StoreNameTags = tagsArray.Items.Select(t => t.Get<string>()).ToList();
					}
				}
			}
		}

		public void Write()
		{
			TomlTable table = Toml.Create();
			table.Add("StoreNameTag", StoreNameTags.ToArray());
			Toml.WriteFile(table, TomlFilePath);
		}
	}
}
