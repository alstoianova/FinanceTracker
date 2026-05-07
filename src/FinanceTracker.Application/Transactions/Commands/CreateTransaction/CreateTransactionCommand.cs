using System;
using MediatR;

namespace FinanceTracker.Application.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand(
    decimal Amount,
    string Description
) : IRequest<Guid>;