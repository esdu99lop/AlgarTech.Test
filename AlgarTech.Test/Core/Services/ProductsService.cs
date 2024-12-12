using AlgarTech.Test.Core.Interfaces;
using AlgarTech.Test.Data.Interfaces;
using AlgarTech.Test.Data.Models;
using AlgarTech.Test.Dto;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AlgarTech.Test.Core.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DataTable> GetAllProductsAsync()
        {
            return await _unitOfWork.ExecuteStoredProcedureAsync("SP_GetAllProducts");
        }

        public async Task InsertProductAsync(ProductsDto product)
        {
            var parameters = new SqlParameter[]
            {
            new SqlParameter("@ProductName", SqlDbType.NVarChar) { Value = product.ProductName },
            new SqlParameter("@Price", SqlDbType.Decimal) { Value = product.Price }
            };

            await _unitOfWork.ExecuteNonQueryAsync("SP_InsertProduct", parameters);
        }

        public async Task UpdateProductAsync(int id, ProductsDto product)
        {
            var parameters = new SqlParameter[]
            {
            new SqlParameter("@IDProduct", SqlDbType.Int) { Value = id },
            new SqlParameter("@ProductName", SqlDbType.NVarChar) { Value = product.ProductName },
            new SqlParameter("@Price", SqlDbType.Decimal) { Value = product.Price }
            };

            await _unitOfWork.ExecuteNonQueryAsync("SP_UpdateProduct", parameters);
        }

        public async Task<DataTable> GetProductByIdAsync(int id)
        {
            var parameters = new SqlParameter[]
            {
            new SqlParameter("@IDProduct", SqlDbType.Int) { Value = id }
            };
            return await _unitOfWork.ExecuteStoredProcedureAsync("SP_GetProductById", parameters);
        }

        public async Task DeleteProductAsync(int id)
        {
            var parameters = new SqlParameter[]
            {
            new SqlParameter("@IDProduct", SqlDbType.Int) { Value = id }
            };

            await _unitOfWork.ExecuteNonQueryAsync("SP_DeleteProduct", parameters);
        }
    }
}
