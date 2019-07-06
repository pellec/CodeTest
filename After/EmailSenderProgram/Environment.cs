using System.Configuration;

namespace EmailSenderProgram
{
	public class Environment
	{
		private readonly string _value;

		private const string Development = "Development";
		private const string Production = "Production";

		public Environment()
		{
			_value = ConfigurationManager.AppSettings["Environment"];
		}

		public bool IsDevelopment() => _value.Equals(Development);
		public bool IsProduction() => _value.Equals(Production);

		public override string ToString()
		{
			return _value;
		}
	}
}