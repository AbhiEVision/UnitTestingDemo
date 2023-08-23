using DemoPractical._Domain.Interface;
using DemoPractical.Models.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoPractical.API.Services
{
	public class JwtServices : IJwtServices
	{
		private IConfiguration _configuration;

		public JwtServices(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GetJwtToken(Employee model, List<string> roles)
		{
			var userRolesCombined = roles.Count() == 0 ? "User" : string.Join(",", roles);
			var jwtTokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

			var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
			{
				Issuer = _configuration.GetValue<string>("Jwt:Issuer"),
				Audience = _configuration.GetValue<string>("Jwt:Audience"),
				Subject = new ClaimsIdentity(new List<Claim>() {
					new Claim("Id", model.Id.ToString()),
					new Claim(JwtRegisteredClaimNames.Email, model.Email!),
					new Claim(JwtRegisteredClaimNames.Name, $"{model.Name}"),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //JWT Token Id
					new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString()), //time at which JWT issued
					new Claim(ClaimTypes.Role,string.Join(",", userRolesCombined))
				}),
				Expires = DateTime.Now.AddDays(5),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
			};
			var token = jwtTokenHandler.CreateToken(tokenDescriptor);
			var jwt = jwtTokenHandler.WriteToken(token);
			return jwt;
		}
	}
}
