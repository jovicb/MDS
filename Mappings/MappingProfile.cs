using AutoMapper;
using Mds.WebApi.Models.Api;
using Mds.WebApi.Models.Db;

namespace Mds.WebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetStocksRequest, StockCalculationRequest>();

            CreateMap<StockCalculationResult, StockResult>()
                .ForMember(x => x.Calculations, src => src.MapFrom(s => s.Calculations))
                .ForMember(x => x.Stocks, src => src.MapFrom(s => s.Stocks))
                .ForMember(x => x.ErrorMessage, src => src.MapFrom(s => s.ErrorMessage));

            CreateMap<Models.Db.StockCalculation, Models.Api.StockCalculation>();
            CreateMap<Models.Db.Stock, Models.Api.Stock>();

            CreateMap<Models.Api.AddStockRequest, Models.Db.AddStockRequest>();


            //CreateMap<GetStockRequest, Mds.WebApi.Models.StockAdministration.GetStockRequest>();

            CreateMap<Models.Api.GetStockRequest, Models.Db.GetStockRequest>();
            CreateMap<Models.Api.AddStockRequest, Models.Db.AddStockRequest>();
            CreateMap<Models.Api.EditStockRequest, Models.Db.EditStockRequest>();
            CreateMap<Models.Api.DeleteStockRequest, Models.Db.DeleteStockRequest>();

            CreateMap<Models.Db.GetStockResponse, Models.Api.GetStockResponse>();
        }
    }
}
