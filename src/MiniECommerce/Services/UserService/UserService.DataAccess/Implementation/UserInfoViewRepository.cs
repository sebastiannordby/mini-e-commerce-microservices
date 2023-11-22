using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Repositories;
using UserService.Library;

namespace UserService.DataAccess.Implementation
{
    internal class UserInfoViewRepository : IUserInfoViewRepository
    {
        private readonly UserDbContext _dbContext;

        public UserInfoViewRepository(
            UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserInfoView?> Get(string userEmail)
        {
            var result = await _dbContext.Users
                .Where(x => x.Email == userEmail)
                .Select(x => new UserInfoView() 
                { 
                    Email = x.Email,
                    FullName = x.FullName,
                    DeliveryAddress = x.DeliveryAddress,
                    DeliveryAddressPostalCode = x.DeliveryAddressPostalCode,
                    DeliveryAddressPostalOffice = x.DeliveryAddressPostalOffice,
                    DeliveryAddressCountry = x.DeliveryAddressCountry,
                    InvoiceAddress = x.InvoiceAddress,
                    InvoicePostalCode = x.InvoicePostalCode,
                    InvoicePostalOffice = x.InvoicePostalOffice,
                    InvoiceAddressCountry = x.InvoiceAddressCountry
                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task Save(UserInfoView infoView)
        {
            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == infoView.Email);

            if(existingUser is not null)
            {
                existingUser.Update(
                    infoView.DeliveryAddress,
                    infoView.DeliveryAddressPostalCode,
                    infoView.DeliveryAddressPostalOffice,
                    infoView.DeliveryAddressCountry,
                    infoView.InvoiceAddress,
                    infoView.InvoicePostalCode,
                    infoView.InvoicePostalOffice,
                    infoView.InvoiceAddressCountry);

                _dbContext.Users.Update(existingUser);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
