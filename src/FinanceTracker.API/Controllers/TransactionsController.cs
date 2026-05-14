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
            .OrderByDescending(t => t.Date)
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
    public async Task<IActionResult> Create(
        [FromBody] Transaction transaction)
    {
        try
        {
            var account =
                await _db.Accounts
                    .FirstOrDefaultAsync(a =>
                        a.Id == transaction.AccountId);

            if (account == null)
            {
                return BadRequest("Account not found");
            }

            transaction.Id = Guid.NewGuid();

            transaction.Date = DateTime.UtcNow;

            if (transaction.Type == "Income")
            {
                account.Balance += transaction.Amount;
            }
            else
            {
                account.Balance -= transaction.Amount;
            }

            _db.Transactions.Add(transaction);

            await _db.SaveChangesAsync();

            var createdTransaction =
                await _db.Transactions
                    .Include(t => t.Account)
                    .Include(t => t.Category)
                    .Where(t => t.Id == transaction.Id)
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
                    .FirstOrDefaultAsync();

            return Ok(createdTransaction);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateTransactionRequest updatedTransaction)
    {
        try
        {
            var transaction =
                await _db.Transactions
                    .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
            {
                return NotFound();
            }

            var oldAccount =
                await _db.Accounts
                    .FirstOrDefaultAsync(a =>
                        a.Id == transaction.AccountId);

            var newAccount =
                await _db.Accounts
                    .FirstOrDefaultAsync(a =>
                        a.Id == updatedTransaction.AccountId);

            if (oldAccount != null)
            {
                if (transaction.Type == "Income")
                {
                    oldAccount.Balance -= transaction.Amount;
                }
                else
                {
                    oldAccount.Balance += transaction.Amount;
                }
            }

            if (newAccount != null)
            {
                if (updatedTransaction.Type == "Income")
                {
                    newAccount.Balance += updatedTransaction.Amount;
                }
                else
                {
                    newAccount.Balance -= updatedTransaction.Amount;
                }
            }

            transaction.Amount =
                updatedTransaction.Amount;

            transaction.Description =
                updatedTransaction.Description;

            transaction.Type =
                updatedTransaction.Type;

            transaction.CategoryId =
                updatedTransaction.CategoryId;

            transaction.AccountId =
                updatedTransaction.AccountId;

            await _db.SaveChangesAsync();

            var editedTransaction =
                await _db.Transactions
                    .Include(t => t.Account)
                    .Include(t => t.Category)
                    .Where(t => t.Id == transaction.Id)
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
                    .FirstOrDefaultAsync();

            return Ok(editedTransaction);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var transaction =
            await _db.Transactions
                .FirstOrDefaultAsync(t => t.Id == id);

        if (transaction == null)
        {
            return NotFound();
        }

        var account =
            await _db.Accounts
                .FirstOrDefaultAsync(a =>
                    a.Id == transaction.AccountId);

        if (account != null)
        {
            if (transaction.Type == "Income")
            {
                account.Balance -= transaction.Amount;
            }
            else
            {
                account.Balance += transaction.Amount;
            }
        }

        _db.Transactions.Remove(transaction);

        await _db.SaveChangesAsync();

        return Ok();
    }

    public class UpdateTransactionRequest
    {
        public decimal Amount { get; set; }

        public string Description { get; set; } = "";

        public string Type { get; set; } = "Expense";

        public Guid AccountId { get; set; }

        public Guid CategoryId { get; set; }
    }
}