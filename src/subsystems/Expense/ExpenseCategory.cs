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
				ExpenseCategory.Food => "�H��",
				ExpenseCategory.DiningOut => "�O�H��",
				ExpenseCategory.Clothing => "�핞��",
				ExpenseCategory.Entertainment => "��y��",
				ExpenseCategory.HouseholdGoods => "���p�i�E�G�ݔ�",
				ExpenseCategory.Subscription => "�T�u�X�N���v�V������",
				_ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
			};
		}
	}
}
