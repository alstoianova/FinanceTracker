namespace FinanceTracker.Application.Statistics.Queries.GetStatistics;

public class StatisticsDto
{
    public decimal TotalIncome { get; set; }

    public decimal TotalExpense { get; set; }

    public decimal Balance => TotalIncome - TotalExpense;
}