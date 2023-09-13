// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace UserService.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] 
        {
            new ApiScope("fullaccess")
        };
        
        public static IEnumerable<ApiResource> ApiResources = new ApiResource[] 
        { 
            new ApiResource("e-commerce", "Mini ECommerce")
            {
                Scopes = { "fullaccess" }
            }
        };
        
        public static IEnumerable<Client> Clients => new Client[] 
        { 
            new Client()
            {
                ClientName = "miniecommerce.desktop",
                ClientId = "miniecommerce.desktop.react",
                ClientSecrets = { new Secret("much-secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "fullaccess" }
            }
        };
    }
}