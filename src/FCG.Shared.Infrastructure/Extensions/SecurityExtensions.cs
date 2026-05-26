using FCG.Shared.Infrastructure.Configurations;
using Microsoft.AspNetCore.Builder;

namespace FCG.Shared.Infrastructure.Extensions
{
	public static class SecurityExtensions
	{
		public static WebApplicationBuilder AddFCGSecurity(this WebApplicationBuilder builder)
		{
			builder.Services.AddIdentityPropagation();

			return builder;
		}
	}
}
