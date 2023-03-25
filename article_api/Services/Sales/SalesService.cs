using article_api.Interfaces.Sales;
using dll.Data;
using dll.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace article_api.Services.Sales
{
    public class SalesService : ISalesService
    {
        private readonly IDataContext _context;
        private readonly ILogger<SalesService> _logger;

        public SalesService(IDataContext context, ILogger<SalesService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(bool isSuccess, string? ErrorMessage)> PostPurchase(Article article)
        {
            try
            {
                if (article == null)
                    return (false, "Article not valid.");

                var purchase = new Purchase
                {
                    PurchaseId = Guid.NewGuid().ToString(),
                    ArticleNumber = article.ArticleNumber,
                    Price = article.Price,
                    CreatedUTC = DateTime.UtcNow,
                };

                _context.Purchases.Add(purchase);
                await _context.SaveChangesAsync();

                return (true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, ex.Message);
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Purchase>? purchases, string? ErrorMessage)> GetAllPurchases()
        {
            try
            {
                var purchases = await _context.Purchases.ToListAsync();

                if (!purchases.Any())
                    return (false, null, "No purchases found.");

                return (true, purchases, null);
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
