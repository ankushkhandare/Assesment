using Moq;
using FluentAssertions;
using SampleAPI.Application.Contracts.Repositories;
using SampleAPI.Application.Features.Order.Commands;
using SampleAPI.Entities;

namespace SampleAPI.Tests.Application.Commands;

public class CreateOrderCommandHandlerTests
{
    
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly CreateOrderCommandHandler _handler;

    public CreateOrderCommandHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _handler = new CreateOrderCommandHandler(_orderRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Create_Order_And_Return_Id()
    {
        // Arrange
        var command = new CreateOrderCommand { Name = "Order1", Description = "Description1" };
        _orderRepositoryMock.Setup(repo => repo.CreateOrderAsync(It.IsAny<Order>()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.GetType().Should().Be(typeof(Guid));
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_Adding_Order_Fails()
    {
        // Arrange
        var command = new CreateOrderCommand { Name = "Order1", Description = "Description1" };
        _orderRepositoryMock.Setup(repo => repo.CreateOrderAsync(It.IsAny<Order>())).ThrowsAsync(new Exception("Error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }
}
