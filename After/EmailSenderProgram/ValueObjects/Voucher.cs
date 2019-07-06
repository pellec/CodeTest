using CSharpFunctionalExtensions;

namespace EmailSenderProgram.ValueObjects
{
	public class Voucher : ValueObject<Voucher>
	{
		private readonly string _value;

		public Voucher(string value)
		{
			_value = value;
		}

		protected override bool EqualsCore(Voucher other)
		{
			return _value.Equals(other._value);
		}

		protected override int GetHashCodeCore()
		{
			return _value.GetHashCode();
		}

		public static explicit operator Voucher(string value)
		{
			return new Voucher(value);
		}

		public override string ToString()
		{
			return _value;
		}
	}
}