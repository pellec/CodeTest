using System;

namespace EmailSenderProgram
{
	public static class Day
	{
		public static DateTime Yesterday => DateTime.Now.AddDays(-1);
	}
}