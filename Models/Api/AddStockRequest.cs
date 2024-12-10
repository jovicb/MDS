namespace Mds.WebApi.Models.Api
{
    public class AddStockRequest
    {
        public int CompanyId { get; set; }
        public DateTime Date { get; set; }
        public float Open { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public float Close { get; set; }
        public float AdjClose { get; set; }
        public float Volume { get; set; }
    }
}
