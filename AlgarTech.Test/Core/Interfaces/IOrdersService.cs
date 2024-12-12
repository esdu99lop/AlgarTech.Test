using AlgarTech.Test.Dto;
using System.Data;

namespace AlgarTech.Test.Core.Interfaces
{
    public interface IOrdersService
    {
        Task<DataTable> GetAllOrdersAsync();
        Task InsertOrderAsync(OrdersDto orderDto);
        Task DeleteOrderAsync(int id);
    }
}
