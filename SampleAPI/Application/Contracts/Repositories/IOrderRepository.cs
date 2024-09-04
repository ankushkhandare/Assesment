using SampleAPI.Entities;

namespace SampleAPI.Application.Contracts.Repositories;

public interface IOrderRepository
{
    public  Task<List<Order>> GetRecentOrdersAsync();
    public Task CreateOrderAsync(Order order);
}