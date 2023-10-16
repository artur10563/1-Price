using System.Globalization;

namespace E_SHOP.Application.Helpers
{
	public static class MonthConverter
	{
		public static string Convert(int month)
		{
			return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
	   }
	}
}
