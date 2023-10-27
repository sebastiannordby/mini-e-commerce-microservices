using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using MiniECommerce.Consumption.Repositories.ProductService;
using MudBlazor;
using ProductService.Library.Models;

namespace DesktopApp.Pages.Admin
{
    public partial class ProductManagement : ComponentBase
    {
        private IEnumerable<ProductView>? _products;
        private ProductDto? _managementProduct;
        private bool _isManagementProductNew;
        private bool _isManagementDialogVisible;

        protected override async Task OnInitializedAsync()
        {
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            _products = await ProductRepository.List(
                null, null, null);
        }

        private void ShowAddProductDialog()
        {
            var takenNumbers = _products
                ?.Select(x => x.Number);

            _managementProduct = new() 
            { 
                Number = takenNumbers?.Any() == true ? 
                    takenNumbers.Max() + 1 : 1
            };
            _isManagementProductNew = true;
            _isManagementDialogVisible = true;
            StateHasChanged();
        }

        private async Task ShowEditProductDialog(ProductView product)
        {
            _managementProduct = await ProductRepository.Find(product.Id);
            _isManagementProductNew = false;
            _isManagementDialogVisible = true;
        }

        private void CancelManagement()
        {
            _isManagementDialogVisible = false;
            _isManagementProductNew = false;
            _managementProduct = null;
        }

        private async Task SubmitManagement()
        {
            if (_managementProduct is null)
                return;

            if(_isManagementProductNew)
            {
                await ProductRepository.Add(_managementProduct);
            }
            else
            {
                await ProductRepository.Update(_managementProduct);
            }

            _isManagementDialogVisible = false;
            _isManagementProductNew = false;
            _managementProduct = null;
            await LoadProducts();
        }

        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private IProductRepository ProductRepository { get; set; }
    }
}
