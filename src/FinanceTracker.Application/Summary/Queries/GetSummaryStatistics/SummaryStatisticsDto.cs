namespace FinanceTracker.Application.Summary.Queries.GetSummaryStatistics;

public class SummaryStatisticsDto
{
    public decimal TotalIncome { get; set; }

    public decimal TotalExpense { get; set; }

    public decimal Balance { get; set; }
}