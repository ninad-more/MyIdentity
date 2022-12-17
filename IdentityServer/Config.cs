using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("PersonAPI", "PersonAPI")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "ConsoleClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret-value".Sha256())
                },
                AllowedScopes = { "PersonAPI" }
            },
            new Client
            {
                ClientId = "WebClient",
                ClientSecrets = { new Secret("secret-value".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
            
                // redirect to after login
                RedirectUris = { "https://localhost:7121/signin-oidc" },

                // redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:7121/signout-callback-oidc" },
                AllowOfflineAccess = true,
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "PersonAPI",
                }
            }
        };
}