namespace Mds.WebApi.Models.Api
{
    public class CalculationResult
    {
        public StockResult? RequestedPeriod { get; set; }
        public StockResult? PreviousPeriod { get; set; }
        public StockResult? NextPeriod { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
