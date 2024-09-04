using Microsoft.EntityFrameworkCore;
using SampleAPI.Application.Contracts.Repositories;
using SampleAPI.Entities;
using SampleAPI.Infrastructure.Data;

namespace SampleAPI.Infrastructure.Repositories;

public class OrderRepository: IOrderRepository
{
    private readonly SampleApiDbContext _context;

    public OrderRepository(SampleApiDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Returns list of Order created in last one day
    /// </summary>
    public async Task<List<Order>> GetRecentOrdersAsync()
    {
        return await _context.Order
            .Where(o => o.EntryDate >= DateTime.UtcNow.AddDays(-1) && !o.IsDeleted)
            .OrderByDescending(o => o.EntryDate)
            .ToListAsync();
    }

    /// <summary>
    /// Creates a new Order
    /// </summary>
    public async Task CreateOrderAsync(Order order)
    {
        _context.Order.Add(order);
        await _context.SaveChangesAsync();
    }
}