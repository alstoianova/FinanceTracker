using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler
    : IRequestHandler<UpdateTransactionCommand, Unit>
{
    private readonly IAppDbContext _context;

    public UpdateTransactionCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        UpdateTransactionCommand request,
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

        transaction.Amount = request.Amount;
        transaction.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}