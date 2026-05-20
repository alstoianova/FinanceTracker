using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Common;

public static class FakeDatabase
{
    public static List<User> Users { get; set; } = new();
}