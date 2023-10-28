using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using Serilog;

namespace DesktopApp.Pages.Identity
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IAuthenticationService _authService;

        public LoginModel(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public IActionResult OnGetAsync(string returnUrl = null)
        {
            string provider = "Google";
            // Request a redirect to the external login provider.
            
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Page("./Login",
                pageHandler: "Callback",
                values: new { returnUrl }),
            };

            return new ChallengeResult(provider, authenticationProperties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(
            string? returnUrl = null, string? remoteError = null)
        {
            var googleUser = this.User.Identities.FirstOrDefault();

            if (googleUser?.IsAuthenticated == true)
            {
                var res = await _authService.AuthenticateAsync(
                    HttpContext, "Google");

                var authenticationResult = await HttpContext.AuthenticateAsync("Google");
                if (authenticationResult.Succeeded)
                {
                    var accessToken = authenticationResult.Properties.GetTokenValue("access_token");
                    var idToken = authenticationResult.Properties.GetTokenValue("id_token");
                    Log.Information("AccessToken: {0}", accessToken);
                    Log.Information("IdToken: {0}", idToken);

                    if(idToken is not null) 
                        HttpContext.Session.SetString("access_token", idToken);
                }
            }

            return LocalRedirect("/");
        }
    }
}