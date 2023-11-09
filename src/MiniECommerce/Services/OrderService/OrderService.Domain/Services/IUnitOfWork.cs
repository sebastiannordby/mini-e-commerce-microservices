using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Services
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
