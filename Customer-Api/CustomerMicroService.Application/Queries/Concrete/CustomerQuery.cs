using CustomerMicroService.Application.Queries.Interfaces;
using CustomerMicroService.Application.Queries.SQL;
using CustomerMicroService.Application.Requests;
using CustomerMicroService.Application.ViewModel;
using CustomerMicroService.Framework.Result.Concrete;
using CustomerMicroService.Framework.Result.Interface;
using CustomerMicroService.Framework.ViewModel;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CustomerMicroService.Application.Queries.Concrete
{
    public class CustomerQuery : ICustomerQuery
    {
        private readonly IConfiguration Configuration;

        public CustomerQuery(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<IApplicationResult<DadosPaginadosViewModel<CustomerViewModel>>> GetByFilterAsync(CustomerFilterRequest request)
        {
            var result = new ApplicationResult<DadosPaginadosViewModel<CustomerViewModel>>();

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@offset", (request.Page - 1) * request.PageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pageSize", request.PageSize, DbType.Int32, ParameterDirection.Input);

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var dadosPaginados = new DadosPaginadosViewModel<CustomerViewModel>(request);


                var data = await connection.QueryAsync<CustomerViewModel, AddressViewModel, CustomerViewModel>(CustomerSql.GetByFilter,
                  (customer, address) =>
                  {

                      customer.Address = address;
                      return customer;
                  },
                  splitOn: "AddressId",
                      param: parameters);

                dadosPaginados.Registros = data.ToList();

                dadosPaginados.Controle.TotalRegistros = data.Count();
                result.Result = dadosPaginados;

                connection.Close();
            }

            return result;
        }

        public async Task<IApplicationResult<CustomerViewModel>> GetByIdAsync(Guid customerId)
        {
            var result = new ApplicationResult<CustomerViewModel>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@customerId", customerId, DbType.Guid, ParameterDirection.Input);

            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var data = await connection.QueryAsync<CustomerViewModel, AddressViewModel, CustomerViewModel>(CustomerSql.GetById,
                (customer, address) =>
                {

                    customer.Address = address;
                    return customer;
                },
                splitOn: "AddressId",
                    param: parameters);

                result.Result = data.FirstOrDefault();


                connection.Close();
            }

            return result;
        }

    }
}