using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Library;

namespace UserService.DataAccess.Repositories
{
    public interface IUserInfoViewRepository
    {
        Task<UserInfoView?> Get(string userEmail);
        Task Save(UserInfoView infoView);
    }
}
