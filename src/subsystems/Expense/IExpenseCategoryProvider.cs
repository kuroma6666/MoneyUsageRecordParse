namespace SonyBankUsageRecordParse.src.subsystems.Expense
{
	public interface IExpenseCategoryProvider
	{
		List<string> LoadCategories();
		void SaveCategories(List<string> categories);
	}

}