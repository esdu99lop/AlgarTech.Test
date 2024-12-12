using AlgarTech.Test.Core.Interfaces;
using AlgarTech.Test.Data.Interfaces;
using AlgarTech.Test.Dto;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AlgarTech.Test.Core.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrdersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DataTable> GetAllOrdersAsync()
        {
            return await _unitOfWork.ExecuteStoredProcedureAsync("SP_GetAllOrders");
        }

        public async Task InsertOrderAsync(OrdersDto orderDto)
        {
            var orderParameters = new SqlParameter[]
            {
            new SqlParameter("@ClientIdentification", SqlDbType.NVarChar) { Value = orderDto.ClientIdentification },
            new SqlParameter("@ClientAddress", SqlDbType.NVarChar) { Value = orderDto.ClientAddress },
            new SqlParameter("@Total", SqlDbType.Decimal) { Value = orderDto.Total }
            };

            var outputOrderId = new SqlParameter("@OrderId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            var allOrderParameters = orderParameters.Concat(new[] { outputOrderId }).ToArray();
            await _unitOfWork.ExecuteNonQueryAsync("SP_InsertOrder", allOrderParameters);
            int orderId = (int)outputOrderId.Value;

            foreach (var product in orderDto.Products)
            {
                var orderDetailParameters = new SqlParameter[]
                {
                new SqlParameter("@IDOrder", SqlDbType.Int) { Value = orderId },
                new SqlParameter("@IDProduct", SqlDbType.Int) { Value = product.IDProduct },
                new SqlParameter("@ProductsQuantity", SqlDbType.Int) { Value = orderDto.Products.Count }
                };

                await _unitOfWork.ExecuteNonQueryAsync("SP_InsertOrderDetail", orderDetailParameters);
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            var parameters = new SqlParameter[]
            {
            new SqlParameter("@IDOrder", SqlDbType.Int) { Value = id }
            };

            await _unitOfWork.ExecuteNonQueryAsync("SP_DeleteOrder", parameters);
        }
    }
}
