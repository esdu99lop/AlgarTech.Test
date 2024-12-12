using AlgarTech.Test.Data.Models;
using AlgarTech.Test.Dto;
using System.Data;

namespace AlgarTech.Test.Core.Interfaces
{
    public interface IProductsService
    {
        Task<DataTable> GetAllProductsAsync();
        Task InsertProductAsync(ProductsDto product);
        Task UpdateProductAsync(int id, ProductsDto product);
        Task<DataTable> GetProductByIdAsync(int id);
        Task DeleteProductAsync(int id);
    }
}
