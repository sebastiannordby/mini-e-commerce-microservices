using BasketService.Library;
using Microsoft.AspNetCore.Components;
using MiniECommerce.Consumption.Repositories.BasketService.Administration;
using MiniECommerce.Consumption.Repositories.ProductService;
using MudBlazor;
using ProductService.Library.Models;

namespace DesktopApp.Pages.Admin
{
    public partial class BasketManagement : ComponentBase
    {
        private IEnumerable<string> _usersInBasket;
        private IEnumerable<ProductView>? _products;
        private IEnumerable<ProductView>? _availableProducts =>
            _products is not null && _selectedUsersBasket is not null ?
                _products.Where(p => !_selectedUsersBasket.Any(b => b.ProductId == p.Id)) : null;

        private string? _selectedUser;
        private IEnumerable<BasketItemView>? _selectedUsersBasket;
        private bool _showDetailsDialog;

        private bool _showAddProductToBasketDialog;

        protected override async Task OnInitializedAsync()
        {
            _usersInBasket = await BasketRepository.GetUsersBaskets();
            _products = await ProductRepository.List();
        }

        private async Task ShowBasketDetails(string userEmail)
        {
            _selectedUsersBasket = await BasketRepository.Get(userEmail);
            _selectedUser = userEmail;
            _showDetailsDialog = true;
        }

        private async Task IncreaseQuantity(BasketItemView item)
        {
            if (_selectedUser is null)
                return;

            _selectedUsersBasket = await BasketRepository
                .IncreaseQuantity(_selectedUser, item.ProductId);
            Snackbar.Add($"{item.ProductName} increased quantity.");
        }

        private async Task DecreaseQuantity(BasketItemView item)
        {
            if (_selectedUser is null)
                return;

            _selectedUsersBasket = await BasketRepository
                .DecreaseQuantity(_selectedUser, item.ProductId);
            Snackbar.Add($"{item.ProductName} decreased quantity.");
        }

        private void ShowAddProductToBasketDialog()
        {
            _showAddProductToBasketDialog = true;
        }

        private async Task AddProductToBasket(ProductView product)
        {
            if (_selectedUser is null)
                return;

            _selectedUsersBasket = await BasketRepository.AddToBasket(_selectedUser, product.Id);
            Snackbar.Add($"{product.Name} added to basket.");
        }

        private async Task RefreshCurrentBasket()
        {
            if (_selectedUser is null)
                return;

            _selectedUsersBasket = await BasketRepository.Get(_selectedUser);
        }

        [Inject] public required IAdminBasketRepository BasketRepository { get; set; }
        [Inject] public required ISnackbar Snackbar { get; set; }
        [Inject] public required IProductRepository ProductRepository { get; set; }
    }
}
