using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _db;

    public CategoriesController(AppDbContext db)
    {
        _db = db;
    }

    // GET: /Categories
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await _db.Categories.ToListAsync();
        return Ok(categories);
    }

    // POST: /Categories
    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        _db.Categories.Add(category);
        await _db.SaveChangesAsync();

        return Ok(category);
    }

    // PUT: /Categories/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Category updatedCategory)
    {
        var category = await _db.Categories.FindAsync(id);

        if (category == null)
            return NotFound("Category not found");

        category.Name = updatedCategory.Name;

        await _db.SaveChangesAsync();

        return Ok(category);
    }

    // DELETE: /Categories/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var category = await _db.Categories.FindAsync(id);

        if (category == null)
            return NotFound("Category not found");

        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();

        return Ok("Category deleted successfully");
    }
}