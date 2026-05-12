using MediatR;

namespace FinanceTracker.Application.Summary.Queries.GetSummaryStatistics;

public record GetSummaryStatisticsQuery
    : IRequest<SummaryStatisticsDto>;