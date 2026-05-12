using FinanceTracker.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace FinanceTracker.Application.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionCommandHandler
    : IRequestHandler<DeleteTransactionCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DeleteTransactionCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        DeleteTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(
                t => t.Id == request.Id,
                cancellationToken);

        if (transaction is null)
        {
            throw new Exception("Transaction not found");
        }

        _context.Transactions.Remove(transaction);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}