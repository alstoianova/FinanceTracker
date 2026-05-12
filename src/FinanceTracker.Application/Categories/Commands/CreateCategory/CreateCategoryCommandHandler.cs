using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;

namespace FinanceTracker.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler
    : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        _context.Categories.Add(category);

        await _context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}