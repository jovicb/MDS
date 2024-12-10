using Mds.WebApi.Models.Db;
//using Mds.WebApi.Models.StockAdministration;

namespace Mds.WebApi.Interfaces
{
    public interface IDbService
    {
        Task<StockCalculationResult> StockCalculation(StockCalculationRequest request);
        Task<bool> AddStock(AddStockRequest request);
        Task<bool> EditStock(EditStockRequest request);
        Task<bool> DeleteStock(DeleteStockRequest request);
        Task<List<GetStockResponse>> GetStock(GetStockRequest request);


    }
}
