using System.Linq;
using System.Net.Mail;
using EmailSenderProgram.Logger;

namespace EmailSenderProgram.MailSender
{
	public class EmptyMailSender : IMailSender
	{
		private readonly ILogger _logger;

		public EmptyMailSender(ILogger logger)
		{
			_logger = logger;
		}

		public void Send(MailMessage mail)
		{
			_logger.Log($"\tSent mail to: {string.Join(",", mail.To.Select(x => x.Address).ToArray())}");
		}
	}
}