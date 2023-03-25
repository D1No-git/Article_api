using article_api.Models.ViewModels;

namespace article_api.Interfaces.Reports
{
    public interface IReportsService : IDisposable
    {
        Task<(bool isSuccess, IEnumerable<SalesReport>? salesReports, string? errorMessage)> GetNumberArticlesSoldPerDay(DateTime? reportDate);
        Task<(bool isSuccess, IEnumerable<RevenueReport>? revenueReports, string? errorMessage)> GetTotalRevenuePerDay(DateTime? reportDate);
        Task<(bool isSuccess, IEnumerable<StatisticsReport>? statisticsReports, string? errorMessage)> GetStatistics();
    }
}
