using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Statistics.Queries.GetStatistics;

public class GetStatisticsQueryHandler
    : IRequestHandler<GetStatisticsQuery, StatisticsDto>
{
    private readonly IAppDbContext _context;

    public GetStatisticsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<StatisticsDto> Handle(
        GetStatisticsQuery request,
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

        return new StatisticsDto
        {
            TotalIncome = income,
            TotalExpense = expense
        };
    }
}