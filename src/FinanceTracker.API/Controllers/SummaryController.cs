using FinanceTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.API.Controllers;

[ApiController]
[Route("summary")]
public class SummaryController : ControllerBase
{
    private readonly AppDbContext _db;

    public SummaryController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("statistics")]
    public async Task<IActionResult> GetStatistics()
    {
        var income = await _db.Transactions
            .Where(t => t.Type == "Income")
            .SumAsync(t => (decimal?)t.Amount) ?? 0;

        var expense = await _db.Transactions
            .Where(t => t.Type == "Expense")
            .SumAsync(t => (decimal?)t.Amount) ?? 0;

        return Ok(new
        {
            TotalIncome = income,
            TotalExpense = expense,
            Balance = income - expense
        });
    }
}