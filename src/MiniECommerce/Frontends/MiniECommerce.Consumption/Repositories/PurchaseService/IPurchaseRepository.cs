using PurchaseService.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories.PurchaseService
{
    public interface IPurchaseRepository
    {
        Task<string> Pay(PaymentCommandDto command);
    }
}
