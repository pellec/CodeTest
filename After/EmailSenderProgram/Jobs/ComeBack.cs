using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmailSenderProgram.Logger;
using EmailSenderProgram.MailSender;
using EmailSenderProgram.ValueObjects;

namespace EmailSenderProgram.Jobs
{
	public class ComeBack : Mail<ComeBack>
	{
		private readonly Environment _env;
		private readonly Voucher _voucher;

		public ComeBack(Environment env, ILogger logger, Voucher voucher, IMailSender mailSender) : base(mailSender, logger)
		{
			_env = env;
			_voucher = voucher;
		}

		protected override bool IsActive()
		{
			if (_env.IsDevelopment())
			{
				return true;
			}

			return DateTime.Now.DayOfWeek == DayOfWeek.Sunday;
		}

		protected override IReadOnlyCollection<EmailAddress> CreateReceivers()
		{
			var orderEmails = DataLayer.ListOrders()
				.Select(x => x.CustomerEmail.ToLowerInvariant())
				.ToArray();

			return DataLayer.ListCustomers()
				.Where(x => x.Email.ToLowerInvariant().NotIn(orderEmails))
				.Select(x => (EmailAddress)x.Email)
				.ToArray();
		}

		protected override Message CreateMessage(EmailAddress to)
		{
			var body = new StringBuilder();
			body.Append($"Hi {to}");
			body.Append("<br>We miss you as a customer. Our shop is filled with nice products. Here is a voucher that gives you 50 kr to shop for.");
			body.Append($"<br>Voucher: {_voucher}");
			body.Append("<br><br>Best Regards,<br>CDON Team");

			return Message.ComeBackMessage(to, body.ToString());
		}
	}
}