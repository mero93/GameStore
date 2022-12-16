using API.Models;

namespace API.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderModel>> GetAllAsync(int appUserId);

        Task<OrderModel> GetByIdAsync(int id);

        Task CreateOrderAsync(OrderModel newOrder);
    }
}
