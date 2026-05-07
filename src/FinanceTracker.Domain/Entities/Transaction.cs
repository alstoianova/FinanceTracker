namespace FinanceTracker.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }

    public decimal Amount { get; set; }

    public string Note { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public string Type { get; set; } = string.Empty;

    public Guid AccountId { get; set; }

    public Account Account { get; set; }

    public Guid CategoryId { get; set; }

    public Category Category { get; set; }
}