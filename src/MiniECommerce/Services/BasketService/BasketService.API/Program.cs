using MiniECommerce.Authentication;
using MiniECommerce.Library.Services;
using BasketService.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddBasketServiceDomainLayer();
builder.AddECommerceLibrary();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors(x =>
        x.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin());
}
else
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseECommerceLibrary();
app.MapControllers();
app.Run();
