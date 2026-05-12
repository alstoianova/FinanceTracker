using MediatR;

namespace FinanceTracker.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommand : IRequest
{
    public Guid Id { get; set; }

    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;
}