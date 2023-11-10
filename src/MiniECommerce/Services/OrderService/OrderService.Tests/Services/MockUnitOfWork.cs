using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.Services
{
    internal class MockUnitOfWork : IUnitOfWork
    {
        public Task BeginTransactionAsync()
        {
            return Task.CompletedTask;
        }

        public Task CommitAsync()
        {
            return Task.CompletedTask;
        }

        public Task RollbackAsync()
        {
            return Task.CompletedTask;
        }
    }
}
