using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;

namespace FinanceTracker.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateAccountCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateAccountCommand request,
        CancellationToken cancellationToken)
    {
        var account = new Account
        {
            Name = request.Name,
            Balance = request.Balance
        };

        _context.Accounts.Add(account);

        await _context.SaveChangesAsync(cancellationToken);

        return account.Id;
    }
}