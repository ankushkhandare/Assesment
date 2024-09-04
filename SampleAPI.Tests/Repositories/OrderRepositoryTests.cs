
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SampleAPI.Entities;
using SampleAPI.Infrastructure.Data;
using SampleAPI.Infrastructure.Repositories;

namespace SampleAPI.Tests.Repositories;
public class OrderRepositoryTests
{
    private readonly DbContextOptions<SampleApiDbContext> _dbContextOptions;
    private readonly SampleApiDbContext _context;
    private readonly OrderRepository _orderRepository;

    public OrderRepositoryTests()
    {
        // Create in-memory database options
        _dbContextOptions = new DbContextOptionsBuilder<SampleApiDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // Initialize context and repository
        _context = new SampleApiDbContext(_dbContextOptions);
        _orderRepository = new OrderRepository(_context);
    }

    [Fact]
    public async Task GetRecentOrdersAsync_Should_Return_RecentOrders()
    {
        // Arrange
        var oldOrder = new Order
        {
            Id = Guid.NewGuid(),
            Name = "Old Order",
            Description = "Old",
            EntryDate = DateTime.UtcNow.AddDays(-2),
            IsInvoiced = true,
            IsDeleted = false
        };
        var recentOrder = new Order
        {
            Id = Guid.NewGuid(),
            Name = "Recent Order",
            Description = "Recent",
            EntryDate = DateTime.UtcNow,
            IsDeleted = false
        };

        _context.Order.AddRange(oldOrder, recentOrder);
        await _context.SaveChangesAsync();

        // Act
        var result = await _orderRepository.GetRecentOrdersAsync();

        // Assert
        result.Should().HaveCount(1);
        result.First().Name.Should().Be("Recent Order");
    }
    
    [Fact]
    public async Task GetRecentOrdersAsync_Should_Return_EmptyResponse_WhenNo_RecentOrders_Exist()
    {
        // Arrange
        var oldOrder = new Order
        {
            Id = Guid.NewGuid(),
            Name = "Old Order",
            Description = "Old",
            EntryDate = DateTime.UtcNow.AddDays(-2),
            IsInvoiced = true,
            IsDeleted = false
        };
        var recentOrder = new Order
        {
            Id = Guid.NewGuid(),
            Name = "Recent Order",
            Description = "Recent",
            EntryDate = DateTime.UtcNow.AddDays(-4),
            IsDeleted = false
        };

        _context.Order.AddRange(oldOrder, recentOrder);
        await _context.SaveChangesAsync();

        // Act
        var result = await _orderRepository.GetRecentOrdersAsync();

        // Assert
        result.Should().HaveCount(0);
    }

    [Fact]
    public async Task CreateOrderAsync_Should_Add_Order_To_Database()
    {
        // Arrange
        var newOrder = new Order
        {
            Id = Guid.NewGuid(),
            Name = "New Order",
            Description = "New Description",
            EntryDate = DateTime.UtcNow,
            IsInvoiced = true,
            IsDeleted = false
        };

        // Act
        await _orderRepository.CreateOrderAsync(newOrder);

        // Assert
        var order = await _context.Order.FindAsync(newOrder.Id);
        order.Should().NotBeNull();
        order.Name.Should().Be("New Order");
        order.Description.Should().Be("New Description");
    }
}
