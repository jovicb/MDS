using Mds.WebApi.Interfaces;
using Mds.WebApi.Models.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mds.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationsController : ControllerBase
    {
        private IStockService _stockService;

        public CalculationsController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        [Route("Stocks")]
        public async Task<IActionResult> GetStocks([FromQuery]GetStocksRequest request)
        {
            var result = await _stockService.GetCalculations(request);

            return Ok(result);
        }
    }
}
