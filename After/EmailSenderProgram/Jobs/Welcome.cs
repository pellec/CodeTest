using System.Collections.Generic;
using System.Linq;
using EmailSenderProgram.Logger;
using EmailSenderProgram.MailSender;
using EmailSenderProgram.ValueObjects;

namespace EmailSenderProgram.Jobs
{
	public class Welcome : Mail<Welcome>
	{
		public Welcome(IMailSender mailSender, ILogger logger) : base(mailSender, logger)
		{
		}

		protected override bool IsActive()
		{
			return true;
		}

		protected override IReadOnlyCollection<EmailAddress> CreateReceivers()
		{
			return DataLayer.ListCustomers()
				.Where(x => x.CreatedDateTime > Day.Yesterday)
				.Select(x => (EmailAddress) x.Email)
				.ToArray();
		}

		protected override Message CreateMessage(EmailAddress to)
		{
			var body =
				$"Hi {to} <br>We would like to welcome you as customer on our site!<br><br>Best Regards,<br>CDON Team";

			return Message.WelcomeMessage(to, body);
		}
	}
}