using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using ProductService.Library.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Authentication.Google;
using MiniECommerce.Consumption.Repositories.ProductService;
using MiniECommerce.Consumption.Repositories.BasketService;
using BasketService.Library;
using MiniECommerce.Consumption.Repositories.OrderService;
using OrderService.Library.Commands;
using System.Security.Claims;

namespace DesktopApp.Pages
{
    public partial class Index : ComponentBase
    {
        private IEnumerable<ProductView> _products;
        private List<BasketItemView> _basketItems = new();
        private string UserEmail =>
            HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? "";


        protected override async Task OnInitializedAsync()
        {
            _products = await ProductRepository.List();
        }

        private async Task AddToBasket(ProductView product)
        {
            _basketItems = await BasketRepository.AddToBasket(
                UserEmail, product.Id);
        }

        private async Task IncreaseQuantity(BasketItemView item)
        {
            //_basketItems = await BasketRepository.IncreaseQuantity(UserEmail, item.ProductId);
        }

        private async Task DecreaseQuantity(BasketItemView item)
        {
            //_basketItems = await BasketRepository.DecreaseQuantity(UserEmail, item.ProductId);
        }

        private async Task StartOrder()
        {
            var orderId = await OrderRepository.Start(new StartOrderCommandDto()
            {
                
            });
        }

        [Inject] private IHttpContextAccessor HttpContextAccessor { get; set; }
        [Inject] private IOrderRepository OrderRepository { get; set; }
        [Inject] private IBasketRepository BasketRepository { get; set; }
        [Inject] private IProductRepository ProductRepository { get; set; }
    }
}
