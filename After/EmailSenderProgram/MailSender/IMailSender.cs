using System.Net.Mail;

namespace EmailSenderProgram.MailSender
{
	public interface IMailSender
	{
		void Send(MailMessage mail);
	}
}