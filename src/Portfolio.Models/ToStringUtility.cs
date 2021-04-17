using System;
using System.Globalization;
using System.Text;

namespace Portfolio.Models
{
	public static class ToStringUtility
	{
		public static string DateToString(DateTimeOffset? dateTimeOffset, bool displayMonth)
		{
			if (dateTimeOffset == null)
			{
				return "Present";
			}

			var sb = new StringBuilder();

			if (displayMonth)
			{
				string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTimeOffset.Value.Month);

				sb.Append(monthName);
				sb.Append(" ");
			}

			sb.Append(dateTimeOffset.Value.Year.ToString());
			return sb.ToString();
		}
	}
}
