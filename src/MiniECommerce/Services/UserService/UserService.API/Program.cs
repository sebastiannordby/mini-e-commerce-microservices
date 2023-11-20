using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Authentication;
using UserService.API;
using UserService.DataAccess;

var builder = WebApplication.CreateBuilder(args);
var sqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(sqlConnectionString))
    throw new ArgumentException("ConnectionStrings:DefaultConnection must be provided.");

builder.Services.AddControllers();
builder.AddECommerceLibrary();
builder.Services.AddUserServiceDataAccessLayer(sqlConnectionString);

var app = builder.Build();

app.UseRouting();
app.UseECommerceLibrary();
app.MapControllers();
app.Run();
