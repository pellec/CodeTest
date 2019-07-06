using System;
using CSharpFunctionalExtensions;

namespace EmailSenderProgram.ValueObjects
{
	public class EmailAddress : ValueObject<EmailAddress>
	{
		private readonly string _value;

		private EmailAddress(string value)
		{
			_value = value;
		}

		protected override bool EqualsCore(EmailAddress other)
		{
			return _value.Equals(other._value, StringComparison.InvariantCultureIgnoreCase);
		}

		protected override int GetHashCodeCore()
		{
			return _value.GetHashCode();
		}

		public static explicit operator EmailAddress(string value)
		{
			return new EmailAddress(value);
		}

		public static implicit operator string(EmailAddress value)
		{
			return value._value;
		}

		public override string ToString()
		{
			return _value;
		}
	}
}