using Microsoft.AspNetCore.Components;
using MiniECommerce.Consumption.Repositories.UserService;
using UserService.Library;

namespace DesktopApp.Pages
{
    public partial class Profile : ComponentBase
    {
        private UserInfoView? _userInfo;

        protected override async Task OnInitializedAsync()
        {
            _userInfo = await UserRepository.Get() ?? new();
        }

        private async Task Save()
        {
            if(_userInfo is not null)
                await UserRepository.Save(_userInfo);
        }

        [Inject] private IUserRepository UserRepository { get; set; }
    }
}
