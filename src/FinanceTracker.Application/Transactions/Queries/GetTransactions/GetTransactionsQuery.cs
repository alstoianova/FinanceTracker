using FinanceTracker.Domain.Entities;
using MediatR;

namespace FinanceTracker.Application.Transactions.Queries.GetTransactions;

public record GetTransactionsQuery(
    int? Month,
    int? Year,
    Guid? CategoryId,
    string? Type
) : IRequest<List<Transaction>>;