using FirewoodAPI.Authentication;
using FirewoodAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirewoodAPI.Services
{
    public class JwtService : IJwtProvider
	{
		private readonly JwtOptions _options;
        public JwtService(IOptions<JwtOptions> jwtOptions)
        {
            _options = jwtOptions.Value;
        }
        public string GenerateJWT(User user)
		{

			var roleClaims = new List<Claim>();
			var userRoles = user.Roles.Select(ur => ur);

            foreach (var item in userRoles)
            {
				roleClaims.Add(new Claim("role", item.Name));
            }

            var claims = new Claim[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				
			}.Union(roleClaims);

			var signingCredentials = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
				SecurityAlgorithms.HmacSha256
			);

			var token = new JwtSecurityToken(_options.Issuer, _options.Audience, claims, null, DateTime.UtcNow.AddHours(1), signingCredentials);

			string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

			return tokenValue;
		}
	}
}
	