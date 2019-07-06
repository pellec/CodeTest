using System.Linq;

namespace EmailSenderProgram
{
	public static class Extensions
	{
		public static bool In<T>(this T value, params T[] values)
		{
			return values.Contains(value);
		}

		public static bool NotIn<T>(this T value, params T[] values)
		{
			return !value.In(values);
		}
	}
}