using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace OnePrice.UI.Extensions
{
	public static class IdentityExtension
	{
		public static IServiceCollection AddIdentitySupport(this IServiceCollection serviceCollection)
		{
			JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

			serviceCollection.AddAuthentication(options =>
			{
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = "oidc";
			})
				.AddCookie("Cookies")
				.AddOpenIdConnect("oidc", options =>
				{

					options.Authority = "https://localhost:5001";
					options.CallbackPath = "/signin-oidc";
					options.SignedOutCallbackPath = "/signout-callback-oidc";

					options.ClientId = "web";
					options.ClientSecret = "secret";
					options.ResponseType = OpenIdConnectResponseType.Code;

					options.UsePkce = true;
					options.SaveTokens = true;

					options.Scope.Clear();
					options.Scope.Add(OpenIdConnectScope.OpenId);
					options.Scope.Add(OpenIdConnectScope.OfflineAccess);
					options.Scope.Add("profile");
					options.Scope.Add("verification");
					options.GetClaimsFromUserInfoEndpoint = true;

				});


			return serviceCollection;
		}
	}
}
