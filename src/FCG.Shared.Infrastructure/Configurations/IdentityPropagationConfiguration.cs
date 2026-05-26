using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace FCG.Shared.Infrastructure.Configurations
{
	public static class IdentityPropagationConfiguration
	{
		public static IServiceCollection AddIdentityPropagation(this IServiceCollection services)
		{
			services.AddHttpContextAccessor();

			services
				.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.MapInboundClaims = false;

					options.TokenValidationParameters = new TokenValidationParameters
					{
						// Confia no Kong para segurança; ignora validações de criptografia locais
						ValidateIssuerSigningKey = false,
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = false,

						RoleClaimType = "role",

						// Apenas lê o token de forma "cega"
						SignatureValidator = (token, parameters) => new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(token),
					};
				});

			services.AddAuthorization();

			return services;
		}
	}
}
