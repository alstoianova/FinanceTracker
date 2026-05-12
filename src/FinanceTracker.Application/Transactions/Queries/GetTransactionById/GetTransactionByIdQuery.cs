using MediatR;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Transactions.Queries.GetTransactionById;

public record GetTransactionByIdQuery(Guid Id)
    : IRequest<Transaction>;