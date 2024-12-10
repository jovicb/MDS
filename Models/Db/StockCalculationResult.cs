namespace Mds.WebApi.Models.Db
{
    public class StockCalculationResult
    {
        public List<StockCalculation>? Calculations { get; set; }
        public List<Stock>? Stocks { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
