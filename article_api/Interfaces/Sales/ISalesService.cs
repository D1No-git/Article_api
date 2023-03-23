using dll.Models;
using Microsoft.AspNetCore.Mvc;

namespace article_api.Interfaces.Sales
{
    public interface ISalesService : IDisposable
    {
        Task<(bool IsSuccess, string? ErrorMessage)> PostPurchase(Article article);
        Task<(bool isSuccess, IEnumerable<Purchase>? purchases, string? ErrorMessage)> GetAllPurchases();
    }
}
