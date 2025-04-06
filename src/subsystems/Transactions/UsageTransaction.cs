using System;

namespace SonyBankUsageRecordParse.src.subsystems.Transactions
{
	public class UsageTransaction
	{
		public DateTime Date { get; }
		public String StoreName { get; }
		public Decimal Amount { get; }
		public Decimal Balance { get; }

		public UsageTransaction(DateTime date, String storeName, Decimal amount, Decimal balance)
		{
			Date = date;
			StoreName = storeName;
			Amount = amount;
			Balance = balance;
		}

		public override String ToString()
		{
			return $"{Date.ToString("yyyy-MM-dd")} | {StoreName} | {Amount} | {Balance}";
		}
	}
}
