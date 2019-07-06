using System;
using System.Configuration;
using CSharpFunctionalExtensions;
using EmailSenderProgram.Jobs;
using EmailSenderProgram.Logger;
using EmailSenderProgram.MailSender;
using EmailSenderProgram.ValueObjects;

namespace EmailSenderProgram
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var env = new Environment();
			var logger = new ConsoleLogger();
			var sender = CreateSender(env, logger);

			logger.Log($"Starting email job application. Environment set to '{env}'");

			var welcome = new Welcome(sender, logger);
			var comeBack = new ComeBack(env, logger, (Voucher) "ComebackToUs", sender);

			var welcomeResult = welcome.Execute();
			var comeBackResult = comeBack.Execute();

			var result = Result.Combine(welcomeResult, comeBackResult);

			logger.Log(result.IsSuccess
				? "All mails are sent, I hope..."
				: "Oops, something went wrong when sending mail (I think...)");

			Console.ReadKey();
		}

		private static IMailSender CreateSender(Environment environment, ILogger logger)
		{
			if (environment.IsProduction())
			{
				return new SmtpMailSender(ConfigurationManager.AppSettings["SmtpHost"]);
			}

			return new EmptyMailSender(logger);
		}
	}
}
