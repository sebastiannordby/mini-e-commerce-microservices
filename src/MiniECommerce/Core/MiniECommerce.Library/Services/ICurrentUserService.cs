using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Authentication.Services
{
    public interface ICurrentUserService
    {
        public string UserEmail { get; }
        public string UserFullName { get; }
    }
}
