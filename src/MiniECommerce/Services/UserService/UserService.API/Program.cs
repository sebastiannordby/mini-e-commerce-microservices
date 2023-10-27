using Microsoft.AspNetCore.Builder;
using MiniECommerce.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.AddECommerceLibrary();

var app = builder.Build();

app.UseRouting();
app.UseECommerceLibrary();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
