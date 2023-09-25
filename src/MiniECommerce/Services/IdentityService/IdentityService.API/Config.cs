﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

namespace IdentityService.API;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("scope1"),
            new ApiScope("scope2"),
        };

    public static IEnumerable<ApiResource> ApiResources => new[]
{
        new ApiResource("order-service")
        {
            Scopes = new List<string> {"order-service.read", "order-service.write"},
            ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
            UserClaims = new List<string> { "role" }
        },
        new ApiResource("product-service")
        {
            Scopes = new List<string> { "product-service.read", "product-service.write"},
            ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
            UserClaims = new List<string> { "role" }
        },
        new ApiResource("purchase-service")
        {
            Scopes = new List<string> { "purchase-service.read", "purchase-service.write"},
            ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
            UserClaims = new List<string> { "role" }
        }
    };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "ecomclient",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "scope1" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" }
            },

            // NextJS - Application
            new Client
            {
                ClientId = "nextjs_web_app",
                ClientName = "NextJs Web App",
                ClientSecrets = {new Secret("0bced1832f9facd485eb66a70fb33094".Sha256())}, // change me!
                AllowedGrantTypes =  new[] { GrantType.AuthorizationCode },

                // where to redirect to after login
                RedirectUris = { "http://localhost:3000/api/auth/callback/sample-identity-server" },
                PostLogoutRedirectUris = { "http://localhost:3000" },
                AllowedCorsOrigins= { "http://localhost:3000" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "scope1",
                    "scope2"
                },
            },

            new Client()
            {
                ClientId = "desktop-app",
                AllowedGrantTypes = GrantTypes.Code,
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                RequirePkce = true,
                RequireClientSecret = false,
                AllowedCorsOrigins = {
                    "https://localhost:58515"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                RedirectUris = { "https://localhost:58515/authentication/login-callback" },
                PostLogoutRedirectUris = { "https://localhost:58515/authentication/logout-callback" }
            }
        };

    public static List<TestUser> TestUsers { get; internal set; } = new()
    {
        new TestUser()
        {
            Username = "admin",
            Password = "admin"            
        }
    };
}
