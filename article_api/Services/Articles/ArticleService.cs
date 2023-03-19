using article_api.Helpers.Articles;
using article_api.Interfaces.Articles;
using dll.Data;
using dll.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static article_api.Helpers.Articles.ArticleServisHelper;

namespace article_api.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IDataContext _context;
        private readonly ILogger<ArticleService> _logger;

        public ArticleService(IDataContext context, ILogger<ArticleService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(bool isSuccess, IEnumerable<Article>? articles, string? ErrorMessage)> GetArticles()
        {
            try
            {
                var articles = await _context.Articles.ToListAsync();

                if (!articles.Any())
                    return (false, null, "No articles found.");

                return (true, articles, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, Article? article, string? ErrorMessage)> GetArticleById(int id)
        {
            try
            {
                var article = await _context.Articles.FindAsync(id);

                if (article == null)
                    return (false, null, $"Article with id: {id} not found.");

                return (true, article, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, Article? article, string? ErrorMessage)> AddArticle(Article request)
        {
            try
            {
                var validationResult = ValidateRequest(request);

                if (!validationResult.isValid)
                    return (false, null, validationResult.errorMessage);

                var article = new Article
                {
                    Name = request.Name,
                    ArticleNumber = request.ArticleNumber,
                    Price = request.Price,
                    CreatedUTC = request.CreatedUTC,
                };

                _context.Articles.Add(article);
                await _context.SaveChangesAsync();

                return (true, article, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, Article? article, string? ErrorMessage)> UpdateArticle(Article request)
        {
            try
            {
                var article = await _context.Articles.FindAsync(request.Id);

                if (article == null)
                    return (false, null, null);

                var validationResult = ValidateRequest(request);

                if (!validationResult.isValid)
                    return (false, null, validationResult.errorMessage);

                article.ArticleNumber = request.ArticleNumber;
                article.Name = request.Name;
                article.Price = request.Price;
                article.CreatedUTC = request.CreatedUTC;

                _context.Articles.Update(article);
                await _context.SaveChangesAsync();

                return (true, article, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, Article? article, string? ErrorMessage)> DeleteArticle(int id)
        {
            try
            {
                var article = await _context.Articles.FindAsync(id);

                if (article == null)
                    return (false, null, null);

                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();

                return (true, article, null);
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
