using FinanceTracker.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Transactions.Queries.GetTransactionById;

public class GetTransactionByIdQueryHandler
    : IRequestHandler<GetTransactionByIdQuery, Domain.Entities.Transaction>
{
    private readonly IAppDbContext _context;

    public GetTransactionByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Transaction> Handle(
        GetTransactionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var transaction = await _context.Transactions
            .Include(t => t.Account)
            .Include(t => t.Category)
            .FirstOrDefaultAsync(
                t => t.Id == request.Id,
                cancellationToken);

        if (transaction is null)
        {
            throw new Exception("Transaction not found");
        }

        return transaction;
    }
}