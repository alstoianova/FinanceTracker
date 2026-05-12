using MediatR;

namespace FinanceTracker.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;

    public decimal Balance { get; set; }
}