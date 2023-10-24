using Microsoft.AspNetCore.Builder;
using MiniECommerce.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddECommerceAuthentication(builder.Configuration);

var app = builder.Build();

app.UseRouting();
app.UseECommerceAutentication();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
