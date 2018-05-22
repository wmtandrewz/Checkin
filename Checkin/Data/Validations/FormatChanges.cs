using System;
using System.Globalization;

namespace Checkin
{
	public class FormatChanges
	{
		public static DateTime PassScanDateFormat(string value)
		{
			DateTime date = DateTime.Today;
			if (value != "")
			{
				date = DateTime.ParseExact(value, "yyMMdd", CultureInfo.InvariantCulture);
			}
			return date;
		}

		public static string changedateformat(string value)
		{
			DateTime date = DateTime.Parse(value);
			String datestring = date.ToString("dd-MM-yyyy");
			return datestring;
		}
	}
}

