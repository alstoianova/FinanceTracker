using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;

namespace FinanceTracker.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateTransactionCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var transaction = new Transaction
{
    Amount = request.Amount,
    Note = request.Description,
    Date = DateTime.UtcNow
};

        _context.Transactions.Add(transaction);

        await _context.SaveChangesAsync(cancellationToken);

        return transaction.Id;
    }
}