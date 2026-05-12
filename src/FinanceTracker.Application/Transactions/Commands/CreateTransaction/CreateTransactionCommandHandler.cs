using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == request.AccountId);

        if (account is null)
        {
            throw new Exception("Account not found");
        }

        account.Balance -= request.Amount;

        var transaction = new Transaction
        {
            Amount = request.Amount,
            Description = request.Description,
            AccountId = request.AccountId,
            CategoryId = request.CategoryId,
            Date = DateTime.UtcNow
        };

        _context.Transactions.Add(transaction);

        await _context.SaveChangesAsync(cancellationToken);

        return transaction.Id;
    }
}