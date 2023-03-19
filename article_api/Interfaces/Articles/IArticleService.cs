using dll.Models;

namespace article_api.Interfaces.Articles
{
    public interface IArticleService : IDisposable
    {
        Task<(bool isSuccess, IEnumerable<Article>? articles, string? ErrorMessage)> GetArticles();
        Task<(bool isSuccess, Article? article, string? ErrorMessage)> GetArticleById(int id);
        Task<(bool isSuccess, Article? article, string? ErrorMessage)> AddArticle(Article request);
        Task<(bool isSuccess, Article? article, string? ErrorMessage)> UpdateArticle(Article request);
        Task<(bool isSuccess, Article? article, string? ErrorMessage)> DeleteArticle(int id);
    }
}
