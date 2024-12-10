using System.Threading.Tasks;
using Mds.WebApi.Models.Api;

namespace Mds.WebApi.Interfaces
{
    public interface IStockService
    {
        Task<CalculationResult> GetCalculations (GetStocksRequest request);
        Task<CrudResult> AddStock (AddStockRequest request);
        Task<List<GetStockResponse>> GetStock(GetStockRequest request);

        Task<CrudResult> DeleteStock (DeleteStockRequest request);
        Task <CrudResult>EditStock (EditStockRequest request);
    }
}
