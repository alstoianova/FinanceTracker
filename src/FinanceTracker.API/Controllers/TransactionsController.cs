using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _db;

    public TransactionsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var transactions = await _db.Transactions
            .Include(t => t.Account)
            .Include(t => t.Category)
            .Select(t => new
            {
                t.Id,
                t.Amount,
                t.Description,
                t.Type,
                t.Date,

                AccountName = t.Account != null
                    ? t.Account.Name
                    : "",

                CategoryName = t.Category != null
                    ? t.Category.Name
                    : ""
            })
            .ToListAsync();

        return Ok(transactions);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Transaction transaction)
    {
        try
        {
            transaction.Id = Guid.NewGuid();

            _db.Transactions.Add(transaction);

            await _db.SaveChangesAsync();

            return Ok(new
            {
                transaction.Id,
                transaction.Amount,
                transaction.Description,
                transaction.Type,
                transaction.Date
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}