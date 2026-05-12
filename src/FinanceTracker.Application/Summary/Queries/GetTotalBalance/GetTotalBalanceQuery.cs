using MediatR;

namespace FinanceTracker.Application.Summary.Queries.GetTotalBalance;

public record GetTotalBalanceQuery : IRequest<decimal>;