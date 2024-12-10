namespace Mds.WebApi.Models.Db
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfEstablishment { get; set; }
        public string StockCode { get; set; }
    }
}
