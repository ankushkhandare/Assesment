using AutoMapper;
using Moq;
using Xunit;
using FluentAssertions;
using SampleAPI.Application.Contracts.Repositories;
using SampleAPI.Application.Features.Order.Queries;
using SampleAPI.Application.MappingProfiles;
using SampleAPI.Entities;

namespace SampleAPI.Tests.Application.Queries;

public class GetRecentOrdersQueryHandlerTests
{

    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly IMapper _mapper;
    private readonly GetRecentOrdersQueryHandler _handler;

    public GetRecentOrdersQueryHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<OrderProfile>(); // Add your AutoMapper profile here
        });

        _mapper = mapperConfig.CreateMapper();
        
        _handler = new GetRecentOrdersQueryHandler(_orderRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task Handle_Should_Return_RecentOrders_When_OrdersExist()
    {
        // Arrange
        var recentOrders = new List<Order>
        {
            new Order { Id = Guid.NewGuid(), Name = "Order1", Description = "Description1", EntryDate = DateTime.Now.AddHours(-1), IsInvoiced = true}
        };
        _orderRepositoryMock.Setup(repo => repo.GetRecentOrdersAsync()).ReturnsAsync(recentOrders);

        // Act
        var result = await _handler.Handle(new GetRecentOrdersQuery(), CancellationToken.None);

        // Assert
        result.Should().HaveCount(1);
        result.First().Name.Should().Be("Order1");
    }

    [Fact]
    public async Task Handle_Should_Return_EmptyList_When_NoOrdersExist()
    {
        // Arrange
        _orderRepositoryMock.Setup(repo => repo.GetRecentOrdersAsync()).ReturnsAsync(new List<Order>());

        // Act
        var result = await _handler.Handle(new GetRecentOrdersQuery(), CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }
}



