using System.Globalization;

namespace OnePrice.Application.Helpers
{
	public static class MonthConverter
	{
		public static string Convert(int month)
		{
			return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
	   }
	}
}
