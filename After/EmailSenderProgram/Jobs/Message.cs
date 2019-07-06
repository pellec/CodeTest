using EmailSenderProgram.ValueObjects;

namespace EmailSenderProgram.Jobs
{
	public class Message
	{
		private const string WelcomeMessageSubject = "Welcome as a new customer!";
		private const string ComeBackMessageSubject = "We miss you as a customer";

		private Message(string to, string subject, string body)
		{
			To = to;
			Subject = subject;
			Body = body;
		}

		public string To { get; }
		public string Subject { get; }
		public string Body { get; }

		public static Message WelcomeMessage(EmailAddress to, string body)
		{
			return new Message(to, WelcomeMessageSubject, body);
		}

		public static Message ComeBackMessage(EmailAddress to, string body)
		{
			return new Message(to, ComeBackMessageSubject, body);
		}
	}
}