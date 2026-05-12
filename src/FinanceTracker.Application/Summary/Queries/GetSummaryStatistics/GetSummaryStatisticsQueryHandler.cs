using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Summary.Queries.GetSummaryStatistics;

public class GetSummaryStatisticsQueryHandler
    : IRequestHandler<GetSummaryStatisticsQuery, SummaryStatisticsDto>
{
    private readonly IAppDbContext _context;

    public GetSummaryStatisticsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<SummaryStatisticsDto> Handle(
        GetSummaryStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        var transactions = await _context.Transactions
            .ToListAsync(cancellationToken);

        var income = transactions
            .Where(t => t.Amount > 0)
            .Sum(t => t.Amount);

        var expense = transactions
            .Where(t => t.Amount < 0)
            .Sum(t => Math.Abs(t.Amount));

        var balance = income - expense;

        return new SummaryStatisticsDto
        {
            TotalIncome = income,
            TotalExpense = expense,
            Balance = balance
        };
    }
}