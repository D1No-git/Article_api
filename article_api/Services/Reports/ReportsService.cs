using article_api.Interfaces.Reports;
using article_api.Models.ViewModels;
using dll.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace article_api.Services.Reports
{
    public class ReportsService : IReportsService
    {
        private readonly IDataContext _context;
        private readonly ILogger<ReportsService> _logger;

        public ReportsService(IDataContext context, ILogger<ReportsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(bool isSuccess, IEnumerable<SalesReport>? salesReports, string? errorMessage)> GetNumberArticlesSoldPerDay(DateTime? reportDate)
        {
            try
            {
                if (reportDate.HasValue)
                {
                    var purchases = await _context.Purchases.Where(x => x.CreatedUTC.Date == reportDate.Value.Date).ToListAsync();

                    if (!purchases.Any())
                        return (false, null, $"No sales reports found for {reportDate.Value.Date}.");

                    var salesReports = new List<SalesReport>
                    {
                        new SalesReport
                        {
                            Date = reportDate.Value.Date,
                            TotalArticlesSold = purchases.Count,
                            TotalUniqueArticlesSold = purchases.Select(x => x.ArticleNumber).Distinct().Count()
                        }
                    };

                    return (true, salesReports, null);
                }
                else
                {
                    var purchases = await _context.Purchases.ToListAsync();
                    var dates = purchases.Select(x => x.CreatedUTC.Date).Distinct();
                    var salesReports = new List<SalesReport>();

                    foreach (var date in dates)
                    {
                        salesReports.Add(new SalesReport
                        {
                            Date = date,
                            TotalArticlesSold = purchases.Where(x => x.CreatedUTC.Date == date).Count(),
                            TotalUniqueArticlesSold = purchases.Where(x => x.CreatedUTC.Date == date).Distinct().Count(),
                        });
                    }

                    return (true, salesReports, null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, IEnumerable<RevenueReport>? revenueReports, string? errorMessage)> GetTotalRevenuePerDay(DateTime? reportDate)
        {
            try
            {
                if (reportDate.HasValue)
                {
                    var purchases = await _context.Purchases.Where(x => x.CreatedUTC.Date == reportDate.Value.Date).ToListAsync();

                    if (!purchases.Any())
                        return (false, null, $"No revenue reports found for {reportDate.Value.Date}.");

                    var revenueReports = new List<RevenueReport>
                    {
                        new RevenueReport
                        {
                            Date = reportDate.Value.Date,
                            Revenue = purchases.Sum(x => x.Price)
                        }
                    };

                    return (true, revenueReports, null);
                }
                else
                {
                    var purchases = await _context.Purchases.ToListAsync();
                    var dates = purchases.Select(x => x.CreatedUTC.Date).Distinct();

                    var revenueReports = new List<RevenueReport>();
                    foreach (var date in dates)
                    {
                        revenueReports.Add(new RevenueReport
                        {
                            Date = date,
                            Revenue = purchases.Where(x => x.CreatedUTC.Date == date).Sum(x => x.Price)
                        });
                    }

                    return (true, revenueReports, null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, IEnumerable<StatisticsReport>? statisticsReports, string? errorMessage)> GetStatistics()
        {
            try
            {
                var purchases = await _context.Purchases.ToListAsync();

                if (!purchases.Any())
                    return (false, null, $"No purchases found at all.");

                var articles = purchases.Select(x => x.ArticleNumber).Distinct();

                var statisticsReports = new List<StatisticsReport>();
                foreach (var article in articles)
                {
                    statisticsReports.Add(new StatisticsReport
                    {
                        ArticleNumber = article,
                        Revenue = purchases.Where(x => x.ArticleNumber == article).Sum(x => x.Price)
                    });
                }

                return (true, statisticsReports, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public void Dispose() => _context.Dispose();
    }
}
