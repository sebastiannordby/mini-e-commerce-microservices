using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Authentication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Repositories;
using UserService.Library;

namespace UserService.Tests.DataAccess
{
    public class UserInfoViewRepositoryTests : BaseUserServiceTest
    {
        [Test]
        public void TestIsRegisteredInDI()
        {
            Assert.IsNotNull(
                Services.GetService<IUserInfoViewRepository>());
        }

        [Test]
        public async Task TestReturnsNull()
        {
            var repo = Services.GetRequiredService<IUserInfoViewRepository>();
            var infoView = await repo.Get(string.Empty);

            Assert.IsNull(infoView);
        }

        [Test]
        public async Task TestDoesReturnSavedValue()
        {
            var repo = Services.GetRequiredService<IUserInfoViewRepository>();
            var currentUserService = Services.GetRequiredService<ICurrentUserService>();
            var infoViewToSaved = new UserInfoView() 
            { 
                Email = currentUserService.UserEmail,
                FullName = currentUserService.UserFullName,
                DeliveryAddress = "DeliveryAddress",
                DeliveryAddressPostalCode = "DeliveryAddressPostalCode",
                DeliveryAddressPostalOffice = "DeliveryAddressPostalOffice",
                DeliveryAddressCountry = "DeliveryAddressCountry",
                InvoiceAddress = "InvoiceAddress",
                InvoiceAddressPostalCode = "InvoicePostalCode",
                InvoicePostalOffice = "InvoicePostalOffice",
                InvoiceAddressCountry = "InvoiceAddressCountry"
            };

            await repo.Save(infoViewToSaved);

            var infoViewSaved = await repo.Get(
                currentUserService.UserEmail);

            Assert.IsNotNull(infoViewSaved);
            Assert.AreEqual(infoViewSaved.Email, infoViewToSaved.Email);
        }

    }
}
