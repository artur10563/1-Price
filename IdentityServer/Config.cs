using Duende.IdentityServer.Models;
using Duende.IdentityServer;
using static System.Formats.Asn1.AsnWriter;
using System.Security.Claims;
using IdentityModel;

public static class Config
{
	public static IEnumerable<IdentityResource> IdentityResources =>
		new List<IdentityResource>
		{
			new IdentityResources.OpenId(),
			new IdentityResources.Profile(),
			new IdentityResource()
			{
				Name = "verification",
				UserClaims = new List<string>
				{
					JwtClaimTypes.Email,
					JwtClaimTypes.EmailVerified
				}
			},

			new IdentityResource()
			{
				Name = "roles",
				UserClaims= new List<string>{"role"}
			}
		};


	public static IEnumerable<ApiScope> ApiScopes =>
		new List<ApiScope>
		{
			new ApiScope("api1", "My API")
		};

	public static IEnumerable<Client> Clients =>
		new List<Client>
		{

			new Client
			{
				ClientId = "web",
				ClientSecrets = { new Secret("secret".Sha256()) },

				AllowedGrantTypes = GrantTypes.Code,

				RedirectUris = { "https://localhost:5002/signin-oidc" },
				PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

				AllowOfflineAccess = true,
				RequirePkce = true,

				AllowedScopes = new List<string>
				{
					IdentityServerConstants.StandardScopes.OpenId,
					IdentityServerConstants.StandardScopes.Profile,
					"verification",
					"roles"
				}
			}

		};
}