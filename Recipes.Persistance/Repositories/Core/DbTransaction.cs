using Microsoft.EntityFrameworkCore.Storage;
using Recipes.Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Persistance.Repositories.Core
{
    public class DbTransaction : ITransaction
    {
        private readonly IDbContextTransaction _tran;
        public DbTransaction(IDbContextTransaction transaction)
        {
            _tran = transaction;
        }
        public Task CommitAsync()
        {
            return _tran.CommitAsync();
        }

        public void Dispose()
        {
            _tran.Dispose();
        }

        public Task RollbackAsync()
        {
            return _tran.RollbackAsync();
        }
    }
}
