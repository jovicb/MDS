namespace Mds.WebApi.Models.Api
{
    public class GetStocksRequest
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? CompanyId { get; set; }
    }
}
