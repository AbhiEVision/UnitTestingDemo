using DemoPractical.Models.Models;

namespace DemoPractical._Domain.Interface
{
	public interface IJwtServices
	{
		string GetJwtToken(Employee model, List<string> roles);
	}
}
