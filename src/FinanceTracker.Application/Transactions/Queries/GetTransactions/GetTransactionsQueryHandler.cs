using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Transactions.Queries.GetTransactions;

public class GetTransactionsQueryHandler
    : IRequestHandler<GetTransactionsQuery, List<Transaction>>
{
    private readonly IAppDbContext _context;

    public GetTransactionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Transaction>> Handle(
        GetTransactionsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Transactions
            .Include(t => t.Account)
            .Include(t => t.Category)
            .AsQueryable();

        if (request.Month.HasValue)
        {
            query = query.Where(t => t.Date.Month == request.Month.Value);
        }

        if (request.Year.HasValue)
        {
            query = query.Where(t => t.Date.Year == request.Year.Value);
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(t => t.CategoryId == request.CategoryId.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }
}