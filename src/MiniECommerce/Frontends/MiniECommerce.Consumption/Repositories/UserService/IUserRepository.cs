using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Library;

namespace MiniECommerce.Consumption.Repositories.UserService
{
    public interface IUserRepository
    {
        Task<UserInfoView?> Get();
        Task Save(UserInfoView infoView);
    }
}
