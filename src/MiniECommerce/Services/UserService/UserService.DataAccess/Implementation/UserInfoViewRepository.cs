﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Models;
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
                    InvoiceAddressPostalCode = x.InvoicePostalCode,
                    InvoiceAddressPostalOffice = x.InvoicePostalOffice,
                    InvoiceAddressCountry = x.InvoiceAddressCountry
                }).FirstOrDefaultAsync();

            return result ?? new UserInfoView() 
            { 
                Email = userEmail
            };
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
                    infoView.InvoiceAddressPostalCode,
                    infoView.InvoiceAddressPostalOffice,
                    infoView.InvoiceAddressCountry);

                _dbContext.Users.Update(existingUser);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                await _dbContext.Users.AddAsync(new UserDao(
                    email: infoView.Email,
                    fullName: infoView.FullName,
                    deliveryAddress: infoView.DeliveryAddress,
                    deliveryAddressPostalCode: infoView.DeliveryAddressPostalCode,
                    deliveryAddressPostalOffice: infoView.DeliveryAddressPostalOffice,
                    deliveryAddressCountry: infoView.DeliveryAddressCountry,
                    invoiceAddress: infoView.InvoiceAddress,
                    invoicePostalCode: infoView.InvoiceAddressPostalCode,
                    invoicePostalOffice: infoView.InvoiceAddressPostalOffice,
                    invoiceAddressCountry: infoView.InvoiceAddressCountry
                ));
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
