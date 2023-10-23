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
            string returnUrl = null, string remoteError = null)
        {
            // Get the information about the user from the external login provider
            var googleUser = this.User.Identities.FirstOrDefault();

            //if (googleUser.IsAuthenticated)
            //{
            //    var authProperties = new AuthenticationProperties
            //    {
            //        IsPersistent = true,
            //        RedirectUri = this.Request.Host.Value,
            //        AllowRefresh = true,
            //        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            //    };

            //    await HttpContext.SignInAsync(
            //        scheme: CookieAuthenticationDefaults.AuthenticationScheme,
            //        principal: new ClaimsPrincipal(googleUser),
            //        properties: authProperties);

            //    var res = await _authService.AuthenticateAsync(
            //        HttpContext, "Google");

            //    var test = await HttpContext.AuthenticateAsync("Google");
            //    if(test.Succeeded)
            //    {
            //        var test1 = test.Properties.GetTokenValue("access_token");
            //        var test2 = test.Properties.GetTokenValue("id_token");
            //        Log.Information("test1: {0}", test1);
            //        Log.Information("test2: {0}", test2);
            //    }


            //    var htConAcc = HttpContext.User.FindFirst("access_token");
            //    var htConId = HttpContext.User.FindFirst("id_token");
            //    Log.Information("htConAcc: {0}", htConAcc);
            //    Log.Information("htConId: {0}", htConId);
                
            //    var goAccTok1 = googleUser.FindFirst("access_token");

            //    if (res.Succeeded)
            //    {
            //        var goIdTok1 = res.Properties.GetTokenValue("id_token");
            //        var goIdTok2 = res.Properties.GetTokenValue("idToken");
            //        var goAccTok2 = res.Properties.GetTokenValue("idToken");

            //        Log.Information("goIdTok1: {0}", goIdTok1);
            //        Log.Information("goIdTok2: {0}", goIdTok2);
            //        Log.Information("goAccTok1: {0}", goAccTok2);
            //        Log.Information("goAccTok2: {0}", goAccTok2);

            //    }
            //}

            return LocalRedirect("/");
        }
    }
}