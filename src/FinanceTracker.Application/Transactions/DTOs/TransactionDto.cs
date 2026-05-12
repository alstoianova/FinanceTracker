namespace FinanceTracker.Application.Transactions.DTOs;

public class TransactionDto
{
    public Guid Id { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string Type { get; set; } = string.Empty;

    public string AccountName { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;
}