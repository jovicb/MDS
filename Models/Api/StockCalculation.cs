namespace Mds.WebApi.Models.Api
{
    public class StockCalculation
    {
        //public int Id { get; set; }
        public float LowPrice { get; set; }
        public float LowPriceClose { get; set; }
        public DateTime LowPriceDate { get; set; }
        public float HighPrice { get; set; }
        public float HighPriceClose { get; set; }
        public DateTime HighPriceDate { get; set; }
        public string Stock { get; set; }
        public float? Profit { get; set; }
        public float? TotalProfit { get; set; }
    }
}
