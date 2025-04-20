using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SonyBankUsageRecordParse.src.subsystems.CSV.Common;
using SonyBankUsageRecordParse.src.subsystems.CSV.ICSVFormat;

namespace SonyBankUsageRecordParse.src.subsystems.CSV
{
	public static class CSVParserFactory
	{
		public static ICSVFormatParser CreateParser(CSVType csvType)
		{
			return csvType switch
			{
				CSVType.SonyBank => new SonyBankCSVParser(),
				CSVType.SMBC => new SMBCCSVParser(),
				_ => throw new ArgumentException("未対応のCSV種別です。")
			};
		}
	}

}
