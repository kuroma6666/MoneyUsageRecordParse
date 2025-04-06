namespace SonyBankUsageRecordParse.src.subsystems.Expense
{
	public enum ExpenseCategory
	{
		None,
		Food,
		DiningOut,
		Clothing,
		Entertainment,
		HouseholdGoods,
		Subscription
	}

	public static class ExpenseCategoryExtensions
	{
		public static string ToFriendlyString(this ExpenseCategory category)
		{
			return category switch
			{
				ExpenseCategory.None => " ",
				ExpenseCategory.Food => "食費",
				ExpenseCategory.DiningOut => "外食費",
				ExpenseCategory.Clothing => "被服費",
				ExpenseCategory.Entertainment => "娯楽費",
				ExpenseCategory.HouseholdGoods => "日用品・雑貨費",
				ExpenseCategory.Subscription => "サブスクリプション費",
				_ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
			};
		}
	}
}
