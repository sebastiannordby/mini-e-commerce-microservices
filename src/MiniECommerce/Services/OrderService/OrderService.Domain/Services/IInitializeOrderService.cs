﻿using OrderService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Services
{
    public interface IInitializeOrderService
    {
        Task<Order> Initialize(
            string buyersFullName,
            string buyersEmailAddress);

        Task<Order> Initialize(
            string buyersFullName,
            string buyersEmailAddress,
            string? deliveryAddress,
            string? deliveryAddressPostalCode,
            string? deliveryAddressPostalOffice,
            string? deliveryAddressCountry,
            string? invoiceAddress,
            string? invoiceAddressPostalCode,
            string? invoiceAddressPostalOffice,
            string? invoiceAddressCountry);
    }
}
