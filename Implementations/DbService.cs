using Azure;
using Mds.WebApi.Interfaces;
using Mds.WebApi.Models.Db;
//using Mds.WebApi.Models.StockAdministration;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes ;
using System.Reflection;

namespace Mds.WebApi.Implementations
{
    public class DbService : IDbService
    {
        private readonly string _connectionString;
        public DbService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MDSConnection");
        }

        public async Task<bool> AddStock(AddStockRequest request)
        {
            try
            {

                using (var connection = new SqlConnection(_connectionString))
                {

                    await connection.OpenAsync();

                    using (var command = new SqlCommand("dbo.StockInsert", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Dodajte parametre ako ih procedura zahteva
                        command.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.NVarChar) { Value = request.CompanyId });
                        command.Parameters.Add(new SqlParameter("@Date", SqlDbType.NVarChar) { Value = request.Date });
                        command.Parameters.Add(new SqlParameter("@Open", SqlDbType.NVarChar) { Value = request.Open });
                        command.Parameters.Add(new SqlParameter("@High", SqlDbType.NVarChar) { Value = request.High });
                        command.Parameters.Add(new SqlParameter("@Low", SqlDbType.NVarChar) { Value = request.Low });
                        command.Parameters.Add(new SqlParameter("@Close", SqlDbType.NVarChar) { Value = request.Close });
                        command.Parameters.Add(new SqlParameter("@AdjClose", SqlDbType.NVarChar) { Value = request.AdjClose });
                        command.Parameters.Add(new SqlParameter("@Volume", SqlDbType.NVarChar) { Value = request.Volume });

                        await command.ExecuteNonQueryAsync();
                    }
                    await connection.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public async Task<StockCalculationResult> StockCalculation(StockCalculationRequest request)
        {
            var response = new StockCalculationResult()
            {
                Calculations = new List<StockCalculation>(),
                Stocks = new List<Stock>()
            };
            try
            {

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("dbo.StockCalculation", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Dodajte parametre ako ih procedura zahteva
                        command.Parameters.Add(new SqlParameter("@DateFrom", SqlDbType.NVarChar) { Value = request.DateFrom });
                        command.Parameters.Add(new SqlParameter("@DateTo", SqlDbType.NVarChar) { Value = request.DateTo });
                        command.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.NVarChar) { Value = request.CompanyId });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var item = new StockCalculation
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    LowPrice = (float)Convert.ToDouble(reader["LowPrice"]),
                                    LowPriceClose = (float)Convert.ToDouble(reader["LowPriceClose"]),
                                    LowPriceDate = Convert.ToDateTime(reader["LowPriceDate"]),
                                    HighPrice = (float)Convert.ToDouble(reader["HighPrice"]),
                                    HighPriceClose = (float)Convert.ToDouble(reader["HighPriceClose"]),
                                    HighPriceDate = Convert.ToDateTime(reader["HighPriceDate"]),
                                    Stock = Convert.ToString(reader["Stock"]),
                                    Profit = reader["Profit"] == DBNull.Value ? null : (float)Convert.ToDouble(reader["Profit"]), //provera da li je vrednost null
                                    TotalProfit = (float)Convert.ToDouble(reader["TotalProfit"])
                                };
                                response.Calculations.Add(item);
                            }


                            if (await reader.NextResultAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var item = new Stock
                                    {
                                        StockId = Convert.ToInt32(reader["StockId"]),
                                        CompanyId = Convert.ToInt32(reader["CompanyId"]),
                                        CompanyName = reader["CompanyName"].ToString(),
                                        Date = Convert.ToDateTime(reader["Date"]),
                                        Open = (float)Convert.ToDouble(reader["Open"]),
                                        High = (float)Convert.ToDouble(reader["High"]),
                                        Low = (float)Convert.ToDouble(reader["Low"]),
                                        Close = (float)Convert.ToDouble(reader["Close"]),
                                        AdjClose = (float)Convert.ToDouble(reader["AdjClose"]),
                                        Volume = Convert.ToInt32(reader["Volume"]),
                                    };
                                    response.Stocks.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        public async Task<bool> EditStock(EditStockRequest request) // to do
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("dbo.StockEdit", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Dodajte parametre ako ih procedura zahteva
                        command.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.NVarChar) { Value = request.CompanyId });
                        command.Parameters.Add(new SqlParameter("@Date", SqlDbType.NVarChar) { Value = request.Date });
                        command.Parameters.Add(new SqlParameter("@Open", SqlDbType.NVarChar) { Value = request.Open });
                        command.Parameters.Add(new SqlParameter("@High", SqlDbType.NVarChar) { Value = request.High });
                        command.Parameters.Add(new SqlParameter("@Low", SqlDbType.NVarChar) { Value = request.Low });
                        command.Parameters.Add(new SqlParameter("@Close", SqlDbType.NVarChar) { Value = request.Close });
                        command.Parameters.Add(new SqlParameter("@AdjClose", SqlDbType.NVarChar) { Value = request.AdjClose });
                        command.Parameters.Add(new SqlParameter("@Volume", SqlDbType.NVarChar) { Value = request.Volume });


                        await command.ExecuteNonQueryAsync();
                    }
                    await connection.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public async Task<bool> DeleteStock(DeleteStockRequest request) // to do
        {
            try
            {

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("dbo.StockDelete", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Dodajte parametre ako ih procedura zahteva
                        command.Parameters.Add(new SqlParameter("@Date", SqlDbType.NVarChar) { Value = request.Date });
                        command.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.NVarChar) { Value = request.CompanyId });


                        await command.ExecuteNonQueryAsync();
                    }
                    await connection.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public async Task<List<GetStockResponse>> GetStock(GetStockRequest request) // to do
        {
            var response = new List<GetStockResponse>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("dbo.StockSelect", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Dodajte parametre ako ih procedura zahteva
                    command.Parameters.Add(new SqlParameter("@DateFrom", SqlDbType.NVarChar) { Value = request.DateFrom });
                    command.Parameters.Add(new SqlParameter("@DateTo", SqlDbType.NVarChar) { Value = request.DateTo });
                    command.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.NVarChar) { Value = request.CompanyId });

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var item = new GetStockResponse
                            {
                                StockId = Convert.ToInt32(reader["StockId"]),
                                CompanyId = Convert.ToInt32(reader["CompanyId"]),
                                CompanyName = Convert.ToString(reader["CompanyName"]),
                                DateOfEstablishment = Convert.ToDateTime(reader["DateOfEstablishment"]),
                                Date = Convert.ToDateTime(reader["Date"]),
                                Open = (float)Convert.ToDouble(reader["Open"]),
                                High = (float)Convert.ToDouble(reader["High"]),
                                Low = (float)Convert.ToDouble(reader["Low"]),
                                Close = (float)Convert.ToDouble(reader["Close"]), //provera da li je vrednost null
                                AdjClose = (float)Convert.ToDouble(reader["AdjClose"]),
                                Volume = Convert.ToInt32(reader["Volume"])
                            };
                            response.Add(item);
                        }


                    }
                    await connection.CloseAsync();
                }

                return response;
            }
        }
    }
}

