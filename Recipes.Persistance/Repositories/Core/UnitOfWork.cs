using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Core;
using Recipes.Domain.Core.Repositories;

namespace Recipes.Persistance.Repositories.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ITransaction> BeginTransactionAsync()
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            return new DbTransaction(transaction);
        }

        public async Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            return await _dbContext.SaveChangesAsync(token);
        }
    }
}
