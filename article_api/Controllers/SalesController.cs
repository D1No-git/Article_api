using article_api.Interfaces.Sales;
using dll.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace article_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpPost]

        public async Task<IActionResult> PostPurchase(Article article)
        {
            try
            {
                if (article != null)
                    await _salesService.PostPurchase(article);

                return Ok($"New purchase: {article}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}, inner exception: {ex.InnerException}");
            }
        }

        [HttpGet]
        [Route("GetAllSales")]
        public async Task<IActionResult> GetAllSales()
        {
            try
            {
                var result = await _salesService.GetAllPurchases();

                if (result.isSuccess)
                    return Ok(result.purchases);

                return NotFound("No purchases found.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}, inner exception: {ex.InnerException}");
            }
        }
    }
}
