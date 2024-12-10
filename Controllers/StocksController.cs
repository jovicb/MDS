using Mds.WebApi.Interfaces;
using Mds.WebApi.Models.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mds.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddStock([FromBody] AddStockRequest request)
        {
            var result = await _stockService.AddStock(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetStocks([FromQuery] GetStockRequest request)
        {
            var result = await _stockService.GetStock(request);

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteStocks([FromBody] DeleteStockRequest request)
        {

            return Ok(await _stockService.DeleteStock(request));
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> EditStocks([FromBody] EditStockRequest request)
        {
            var result = await _stockService.EditStock(request);

            return Ok(result);
        }
    }
}
