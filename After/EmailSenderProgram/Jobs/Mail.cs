using System;
using System.Collections.Generic;
using System.Net.Mail;
using CSharpFunctionalExtensions;
using EmailSenderProgram.Logger;
using EmailSenderProgram.MailSender;
using EmailSenderProgram.ValueObjects;
using static CSharpFunctionalExtensions.Result;

namespace EmailSenderProgram.Jobs
{
	public abstract class Mail<TJob> where TJob : Mail<TJob>
	{
		private readonly ILogger _logger;
		private readonly IMailSender _sender;

		protected Mail(IMailSender sender, ILogger logger)
		{
			_sender = sender;
			_logger = logger;
		}

		public Result Execute()
		{
			_logger.Log($"'{typeof(TJob).Name}' - Started job");

			if (!IsActive())
			{
				_logger.Log($"'{typeof(TJob).Name}' - Job not active, exiting'");

				return Ok();
			}

			foreach (var address in CreateReceivers())
			{
				var mail = CreateMessage(address);

				var result = Send(mail.Subject, mail.Body, mail.To);
				if (result.IsFailure) return result;
			}

			_logger.Log($"'{typeof(TJob).Name}' - Finished job");

			return Ok();
		}

		protected abstract bool IsActive();
		protected abstract IReadOnlyCollection<EmailAddress> CreateReceivers();
		protected abstract Message CreateMessage(EmailAddress to);

		protected Result Send(string subject, string body, string addresses)
		{
			using (var mail = new MailMessage())
			{
				mail.To.Add(addresses);
				mail.Subject = subject;
				mail.From = new MailAddress("info@cdon.com");
				mail.Body = body;

				try
				{
					_sender.Send(mail);
				}
				catch (Exception e)
				{
					return Result.Fail(e.ToString());
				}

				return Ok();
			}
		}
	}
}