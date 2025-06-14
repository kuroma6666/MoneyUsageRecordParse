namespace SonyBankUsageRecordParse.src.subsystems.Expense
{
	public class ExpenseCategoryManager
	{
		private readonly IExpenseCategoryProvider _provider;
		private List<string> _categories;

		public ExpenseCategoryManager(IExpenseCategoryProvider provider)
		{
			_provider = provider;
			_categories = _provider.LoadCategories();
		}

		public List<string> GetCategories()
		{
			return _categories;
		}

		public void AddCategory(string category)
		{
			if (!_categories.Contains(category))
			{
				_categories.Add(category);
				_provider.SaveCategories(_categories);
			}
		}

		public void RemoveCategory(string category)
		{
			if (_categories.Contains(category))
			{
				_categories.Remove(category);
				_provider.SaveCategories(_categories);
			}
		}
	}

}