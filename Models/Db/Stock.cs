namespace Mds.WebApi.Models.Db
{
    public class Stock
    {
        public int StockId { get; set; }
        public int CompanyId { get; set; }
        //public Company Company { get; set; }
        public string CompanyName { get; set; }
        public DateTime Date { get; set; }
        public float Open { get; set; }
        public float High {  get; set; }
        public float Low { get; set; }
        public float Close { get; set; }
        public float AdjClose { get; set; }
        public long Volume { get; set; }
    }
}
