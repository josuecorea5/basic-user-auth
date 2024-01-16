using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace FirewoodAPI.Authentication.JwtOptionsSetup
{
	public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
	{
		private JwtOptions _jwtOptions;

		public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
		{
			_jwtOptions = jwtOptions.Value;
		}

		public void Configure(JwtBearerOptions options)
		{
			options.TokenValidationParameters = new()
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = _jwtOptions.Issuer,
				ValidAudience = _jwtOptions.Audience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
			};

			options.Events = new JwtBearerEvents()
			{
				OnChallenge = context =>
				{
					context.HandleResponse();
					context.Response.StatusCode = 401;
					context.Response.ContentType = "application/json";
					var result = JsonSerializer.Serialize(new { error = context.Error});

					return context.Response.WriteAsync(result);
				},

				OnForbidden = context =>
				{
					context.Response.StatusCode = 400;
					context.Response.ContentType = "application/json";
					var result = JsonSerializer.Serialize(context.Response.ToString());

					return context.Response.WriteAsync(result);
				},
			};
		}

		public void Configure(string? name, JwtBearerOptions options)
			=> Configure(options);
	}
}
