﻿namespace Mds.WebApi.Models.Db
{
    public class GetStockRequest
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int? CompanyId { get; set; }
    }
}
