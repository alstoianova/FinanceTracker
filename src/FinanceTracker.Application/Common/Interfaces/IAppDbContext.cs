using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Transaction> Transactions { get; }

    DbSet<Account> Accounts { get; }

    DbSet<Category> Categories { get; }

    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken);
}