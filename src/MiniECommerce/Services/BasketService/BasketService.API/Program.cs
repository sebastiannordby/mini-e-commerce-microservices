using MiniECommerce.Authentication;
using MiniECommerce.Gateway.Consumption;
using BasketService.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGatewayConsumption();
builder.Services.AddBasketServiceDomainLayer();
builder.Services.AddECommerceAuthentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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
app.UseECommerceAutentication();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
