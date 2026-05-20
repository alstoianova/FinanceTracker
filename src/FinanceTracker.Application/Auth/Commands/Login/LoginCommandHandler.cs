using FinanceTracker.Application.Common;
using MediatR;

namespace FinanceTracker.Application.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = FakeDatabase.Users
            .FirstOrDefault(x => x.Email == request.Email);

        if (user == null)
        {
            return "User not found";
        }

        var passwordValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash);

        if (!passwordValid)
        {
            return "Invalid password";
        }

        return "Login successful";
    }
}