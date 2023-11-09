using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DataAccess.Services
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly OrderDbContext _context;
        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(OrderDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _currentTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _currentTransaction?.CommitAsync();
            }
            catch
            {
                // Rollback the transaction if any exception occurs during saving the changes
                await RollbackAsync();
                throw; // It's important to rethrow the exception
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public void Dispose()
        {
            if (_currentTransaction is not null)
                _currentTransaction.Dispose();

            _context.Dispose();
        }
    }
}
