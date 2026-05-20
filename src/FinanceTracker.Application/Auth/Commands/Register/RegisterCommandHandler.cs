using FinanceTracker.Application.Common;
using FinanceTracker.Domain.Entities;
using MediatR;

namespace FinanceTracker.Application.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = FakeDatabase.Users.FirstOrDefault(x => x.Email == request.Email);

        if (existingUser != null)
        {
            return "User already exists";
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow
        };

        FakeDatabase.Users.Add(user);

        return "User registered successfully";
    }
}