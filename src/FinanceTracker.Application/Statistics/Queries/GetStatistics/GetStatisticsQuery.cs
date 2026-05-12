using MediatR;

namespace FinanceTracker.Application.Statistics.Queries.GetStatistics;

public record GetStatisticsQuery
    : IRequest<StatisticsDto>;