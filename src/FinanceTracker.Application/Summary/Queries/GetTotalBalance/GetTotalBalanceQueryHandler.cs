using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Summary.Queries.GetTotalBalance;

public class GetTotalBalanceQueryHandler
    : IRequestHandler<GetTotalBalanceQuery, decimal>
{
    private readonly IAppDbContext _context;

    public GetTotalBalanceQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<decimal> Handle(
        GetTotalBalanceQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Accounts
            .SumAsync(x => x.Balance, cancellationToken);
    }
}