using DemoPractical.Models.DTOs;

namespace DemoPractical.Domain.Interface
{
	public interface IEmailService
	{
		bool SendMail(EmailModel model);
	}
}