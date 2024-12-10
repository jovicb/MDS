namespace Mds.WebApi.Models.Db
{
    public class StockCalculationRequest
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? CompanyId { get; set; }
    }
}
