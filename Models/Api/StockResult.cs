namespace Mds.WebApi.Models.Api
{
    public class StockResult
    {
        public List<StockCalculation>? Calculations { get; set; }
        public List<Stock>? Stocks { get; set; }
        public string? ErrorMessage { get; set; }

        /*
         * 
        
        public List<StockCalculation>? Calculations { get; set; }
        public List<Stock>? Stocks { get; set; }
        public StockResult? PreviousPeriod { get; set; }
        public StockResult? NextPeriod { get; set; }
        public string? ErrorMessage { get; set; }
         * */
    }
}
