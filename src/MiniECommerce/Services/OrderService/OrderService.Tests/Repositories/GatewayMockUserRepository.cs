using MiniECommerce.Library.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Library;

namespace OrderService.Tests.Repositories
{
    internal class GatewayMockUserRepository : IGatewayUserRepository
    {
        public async Task<UserInfoView?> Find(string userEmailAddress)
        {
            return await Task.FromResult(new UserInfoView(
                email: userEmailAddress,
                fullName: userEmailAddress,
                deliveryAddress: nameof(GatewayMockUserRepository),
                deliveryAddressPostalCode: "1920",
                deliveryAddressPostalOffice: "Sørumsand",
                deliveryAddressCountry: "Norge",
                invoiceAddress: nameof(GatewayMockUserRepository),
                invoiceAddressPostalCode: "1920",
                invoiceAddressPostalOffice: "Sørumsand",
                invoiceAddressCountry: "Norge"
            ));
        }
    }
}
