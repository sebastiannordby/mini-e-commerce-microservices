using OrderService.Domain.Models;
using OrderService.Library.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderService.Domain.Models.Order;

namespace OrderService.Domain.Services
{
    public interface ILoadOrderService
    {
        /// <summary>
        /// Allows to work on an Order from existing data.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="number"></param>
        /// <param name="orderLines"></param>
        /// <returns></returns>
        Task<Order> LoadAsync(
            Guid id,
            int number,
            OrderStatus status,
            string buyersName,
            string buyersEmailAddress,
            string? addressLine,
            string? postalCode,
            string? postalOffice,
            string? country,
            IEnumerable<OrderLine> orderLines);

    }
}
