using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Application.Transactions.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Transactions.Queries.GetTransactions;

public class GetTransactionsQueryHandler
    : IRequestHandler<GetTransactionsQuery, List<TransactionDto>>
{
    private readonly IAppDbContext _context;

    public GetTransactionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TransactionDto>> Handle(
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

        if (!string.IsNullOrWhiteSpace(request.Type))
        {
            query = query.Where(t => t.Category.Name == request.Type);
        }

        return await query
            .Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Date = t.Date,
                Type = t.Category.Name,
                AccountName = t.Account.Name,
                CategoryName = t.Category.Name
            })
            .ToListAsync(cancellationToken);
    }
}