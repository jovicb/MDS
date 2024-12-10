using AutoMapper;
using Mds.WebApi.Interfaces;
using Mds.WebApi.Models.Api;
using Mds.WebApi.Models.Db;

namespace Mds.WebApi.Implementations
{
    public class StockService : IStockService
    {
        public IDbService _dbService { get; set; }
        private readonly IMapper _mapper;

        public StockService(IDbService dbService, IMapper mapper)
        {
            _dbService = dbService;
            _mapper = mapper;
        }

        public async Task<CalculationResult> GetCalculations(GetStocksRequest request)
        {
            var result = new CalculationResult();
            try
            {
                var getCalcsFromDb = _mapper.Map<StockCalculationRequest>(request);
                var calculationResult = await _dbService.StockCalculation(getCalcsFromDb);
                result.RequestedPeriod = _mapper.Map<StockResult>(calculationResult);

                var diffDays = (request.DateTo - request.DateFrom).TotalDays;
                var dateFromPrevious = request.DateFrom.AddDays(-diffDays - 1);
                var dateToPrevious = dateFromPrevious.AddDays(diffDays);

                var getPreviousCalcsFromDb = new StockCalculationRequest
                {
                    DateFrom = dateFromPrevious,
                    DateTo = dateToPrevious,
                    CompanyId = request.CompanyId
                };
                var previousCalculationResult = await _dbService.StockCalculation(getPreviousCalcsFromDb);
                result.PreviousPeriod = _mapper.Map<StockResult>(previousCalculationResult);


                var dateFromNext = request.DateTo.AddDays(1);
                var dateToNext = dateFromNext.AddDays(diffDays);

                var getNextCalcsFromDb = new StockCalculationRequest
                {
                    DateFrom = dateFromNext,
                    DateTo = dateToNext,
                    CompanyId = request.CompanyId
                };
                var nextCalculationResult = await _dbService.StockCalculation(getNextCalcsFromDb);
                result.NextPeriod = _mapper.Map<StockResult>(nextCalculationResult);

                return result;
            }
            catch (Exception ex) 
            {
                return new CalculationResult
                {
                    ErrorMessage = ex.Message,
                };
            }
        }

        public async Task<List<Models.Api.GetStockResponse>> GetStock(Models.Api.GetStockRequest request)
        {
            var response = new List<Models.Api.GetStockResponse>();

            try
            {
                var getStockDb = _mapper.Map<Models.Db.GetStockRequest>(request);
                var dbResult = await _dbService.GetStock(getStockDb);
                if (dbResult.Count() == 0)
                {
                    return response;
                }

                response = _mapper.Map<List<Models.Api.GetStockResponse>>(dbResult);


            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<CrudResult> AddStock(Models.Api.AddStockRequest request)
        {
            try
            {
                var addStockDb = _mapper.Map<Models.Db.AddStockRequest>(request);
                var dbResult = await _dbService.AddStock(addStockDb);
                if (!dbResult)
                {
                    return new CrudResult
                    {
                        Error = "Insert failed",
                        Success = false
                    };
                }

                return new CrudResult
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new CrudResult
                {
                    Error = ex.Message,
                    Success = false
                };
            }
        }

        public async Task<CrudResult> EditStock(Models.Api.EditStockRequest request) // edit
        {
            try
            {
                var editStockDb = _mapper.Map<Models.Db.EditStockRequest>(request);
                var dbResult = await _dbService.EditStock(editStockDb);
                if (!dbResult)
                {
                    return new CrudResult
                    {
                        Error = "Insert failed",
                        Success = false
                    };
                }

                return new CrudResult
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new CrudResult
                {
                    Error = ex.Message,
                    Success = false
                };
            }
        }

        public async Task<CrudResult> DeleteStock(Models.Api.DeleteStockRequest request) // delete
        {
            try
            {
                var deleteStockDb = _mapper.Map<Models.Db.DeleteStockRequest>(request);
                var dbResult = await _dbService.DeleteStock(deleteStockDb);
                if (!dbResult)
                {
                    return new CrudResult
                    {
                        Error = "Insert failed",
                        Success = false
                    };
                }

                return new CrudResult
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new CrudResult
                {
                    Error = ex.Message,
                    Success = false
                };
            }
        }
    }
}
