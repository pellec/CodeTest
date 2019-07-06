using System.Net.Mail;

namespace EmailSenderProgram.MailSender
{
	public class SmtpMailSender : IMailSender
	{
		private readonly string _host;

		public SmtpMailSender(string host)
		{
			_host = host;
		}

		public void Send(MailMessage mail)
		{
			var smtp = new SmtpClient(_host);
			smtp.Send(mail);
		}
	}
}