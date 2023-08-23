using DemoPractical.Domain.Interface;
using DemoPractical.Models.DTOs;
using DemoPractical.Models.ViewModel;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace DemoPractical.API.Services
{
	public class EmailService : IEmailService
	{
		private readonly EmailConfigurations _configurations;

		public EmailService(EmailConfigurations configurations)
		{
			_configurations = configurations;
		}


		public bool SendMail(EmailModel model)
		{
			MimeMessage createMessage = GetMimeMessage(model);
			return SendMessageUsingSmtpClient(createMessage);

		}

		private bool SendMessageUsingSmtpClient(MimeMessage messgaemail)
		{
			using (SmtpClient client = new SmtpClient())
			{
				try
				{
					client.Connect(_configurations.SmtpServer, _configurations.Port, true);
					client.Authenticate(_configurations.From, _configurations.Password);
					client.Send(messgaemail);
					return true;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					return false;
				}
				finally
				{
					client.Disconnect(true);
					client.Dispose();
				}
			}
		}

		private MimeMessage GetMimeMessage(EmailModel model)
		{
			MimeMessage message = new MimeMessage();

			message.From.Add(new MailboxAddress(_configurations.UserName, _configurations.From));
			message.To.Add(new MailboxAddress(model.UserName, model.To));
			message.Body = new TextPart(TextFormat.Html) { Text = model.Body };
			message.Subject = model.Subject;

			return message;
		}

	}
}