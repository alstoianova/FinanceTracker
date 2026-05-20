using MediatR;

namespace FinanceTracker.Application.Auth.Commands.Register;

public class RegisterCommand : IRequest<string>
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}