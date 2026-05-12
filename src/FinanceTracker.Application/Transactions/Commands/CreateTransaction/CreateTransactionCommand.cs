using MediatR;

namespace FinanceTracker.Application.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand(
    decimal Amount,
    string Description,
    Guid AccountId,
    Guid? CategoryId
) : IRequest<Guid>;