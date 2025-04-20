using SonyBankUsageRecordParse.src.subsystems.Transactions;

namespace SonyBankUsageRecordParse.src.subsystems.CSV
{
	public interface ICSVFormatParser
	{
		List<UsageTransaction> Parse(StreamReader reader);
	}

}
