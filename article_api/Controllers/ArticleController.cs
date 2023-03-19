using article_api.Interfaces.Articles;
using dll.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace article_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [Route("GetArticles")]
        public async Task<IActionResult> GetArticles()
        {
            try
            {
                var result = await _articleService.GetArticles();

                if (result.isSuccess)
                    return Ok(result.articles);

                return NotFound("No articles found!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}, inner exception: {ex.InnerException}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById(int id)
        {
            try
            {
                var result = await _articleService.GetArticleById(id);

                if (result.isSuccess)
                    return Ok(result.article);

                return NotFound("Article not found!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}, inner exception: {ex.InnerException}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(Article request)
        {
            try
            {
                var result = await _articleService.AddArticle(request);

                if (result.isSuccess)
                    return Ok(result.article);

                return NotFound("Article not created!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}, inner exception: {ex.InnerException}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArticle(Article request)
        {
            try
            {
                var result = await _articleService.UpdateArticle(request);

                if (result.isSuccess)
                    return Ok(result.article);

                return NotFound("Article not found!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}, inner exception: {ex.InnerException}");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            try
            {
                var result = await _articleService.DeleteArticle(id);

                if (result.isSuccess)
                    return Ok(result.article);

                return NotFound("Article not found!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}, inner exception: {ex.InnerException}");
            }
        }
    }
}
