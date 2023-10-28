using DesktopApp.Areas.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using MiniECommerce.Consumption;
using MudBlazor.Services;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Polly;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

builder.Host.UseSerilog((ctx, lc) =>
{
    lc
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
        .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
        .MinimumLevel.Override("System", LogEventLevel.Warning)
        .WriteTo.Console(
            outputTemplate:
                "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:l}{NewLine}{Exception}{NewLine}",
            theme: AnsiConsoleTheme.Code)
        .Enrich.FromLogContext();
});

var googleClientId = configuration["Authentication:Google:ClientId"] ?? 
    throw new Exception("Configuration: Authentication:Google:ClientId must be provided");
var googleClientSecret = configuration["Authentication:Google:ClientSecret"] ?? 
    throw new Exception("Configuration: Authentication:Google:ClientSecret must be provided");

Log.Information("GoogleClientId: " + googleClientId);
Log.Information("GoogleClientSecret: " + googleClientSecret);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Identity/Login"; // Specify your login page
})
.AddOpenIdConnect("Google", options =>
{
    options.Authority = "https://accounts.google.com";
    options.ClientId = googleClientId;
    options.ClientSecret = googleClientSecret;
    options.RefreshInterval = TimeSpan.FromMinutes(5);
    options.ResponseType = "code id_token token";
    options.CallbackPath = "/signin-google"; // Specify your callback path
    options.SignedOutCallbackPath = "/signout-callback-google"; // Specify your sign-out callback path
    options.Scope.Add("openid");
    options.Scope.Add("email");
    options.SaveTokens = true; // Save tokens received from the authentication server

    // Configure any additional OpenID Connect options
    options.Events = new OpenIdConnectEvents
    {
        OnTokenResponseReceived = context =>
        {
            var user = context?.Principal;

            if(user is not null && user.Identity is not null)
            {
                // Optionally, customize token response handling
                // Add or update claims as needed
                user.AddIdentity(new ClaimsIdentity(
                    user.Claims,
                    user.Identity.AuthenticationType,
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType));
            }

            return Task.CompletedTask;
        }
    };
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.Authority = "https://accounts.google.com";
    options.Audience = googleClientId;
    options.SaveToken = true;
});

builder.Services.AddAuthorization();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<HttpContextAccessor>();
builder.Services.AddConsumptionLayer();
builder.Services.AddMudServices();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Log.Information("DesktopApp running in development.");
    app.UseDeveloperExceptionPage();
} 
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy(new CookiePolicyOptions
{
    Secure = CookieSecurePolicy.Always
});
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapBlazorHub();
    endpoints.MapRazorPages();
    endpoints.MapFallbackToPage("/_Host").RequireAuthorization();
});
app.Run();


//app.Use(async delegate (HttpContext context, Func<Task> next)
//{
//    if (context.User.Identity!.IsAuthenticated && 
//        !context.Request.Path.Value.Contains("signin-google"))
//    {
//        foreach (string key in context.Request.Cookies.Keys)
//            context.Response.Cookies.Delete(key);

//        context.Response.Redirect("/");
//    }

//    await next();
//});