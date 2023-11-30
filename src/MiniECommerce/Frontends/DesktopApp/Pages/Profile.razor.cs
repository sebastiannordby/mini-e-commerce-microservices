using Microsoft.AspNetCore.Components;
using MiniECommerce.Consumption.Repositories.UserService;
using MudBlazor;
using UserService.Library;

namespace DesktopApp.Pages
{
    public partial class Profile : ComponentBase
    {
        private UserInfoView? _userInfo;

        protected override async Task OnInitializedAsync()
        {
            _userInfo = await UserRepository.Get() ?? new() 
            { 
                
            };
        }

        private async Task Save()
        {
            if (_userInfo is null)
                return;

            await UserRepository.Save(_userInfo);
            _userInfo = await UserRepository.Get();
            Snackbar.Add("Your information is updated");
        }

        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] private IUserRepository UserRepository { get; set; }
    }
}
