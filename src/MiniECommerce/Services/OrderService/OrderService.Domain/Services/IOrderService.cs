using OrderService.Domain.Models;
using OrderService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Services
{
    public interface IOrderService
    {
        Task<Order?> FindAsync(Guid id);
        Task<Guid> SaveAsync(Order order);
        Task<int> GetNewNumberAsync();
        Task<bool> HasOrderInProgressAsync(string buyersEmailAddress);
        Task<Guid?> GetStartedOrderIdAsync(string buyersEmailAddress);
        Task<bool> SetWaitingForConfirmationAsync(string buyersEmailAddress);
        Task<bool> SetDeliveryAddressAsync(
            string buyersEmailAddress,
            string addressLine, 
            string postalCode, 
            string postalOffice, 
            string country);

        Task<bool> SetInvoiceAddressAsync(
            string buyersEmailAddress,
            string addressLine,
            string postalCode,
            string postalOffice,
            string country);
    }
}
