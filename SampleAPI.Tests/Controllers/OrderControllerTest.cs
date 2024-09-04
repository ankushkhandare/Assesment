
using Moq;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using MediatR;
using SampleAPI.Application.Features.Order.Commands;
using SampleAPI.Application.Features.Order.Queries;
using SampleAPI.Controllers;

namespace SampleAPI.Tests.Controllers;

public class OrderControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly OrderController _controller;

    public OrderControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new OrderController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetRecentOrders_Should_Return_Ok_When_OrdersExist()
    {
        // Arrange
        var orders = new List<OrderDto> { new OrderDto { Name = "Order1", Description = "some Description", Id = Guid.NewGuid(), EntryDate = DateTime.Today, IsInvoiced = true} };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetRecentOrdersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(orders);

        // Act
        var response = await _controller.GetRecentOrders();

        // Assert
        
        response.Result.Should().BeOfType<OkObjectResult>();
        var okResult = response.Result as OkObjectResult;
        okResult?.Value.Should().BeEquivalentTo(orders);
    }

    [Fact]
    public async Task GetRecentOrders_Should_Return_NoContent_When_NoOrdersExist()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetRecentOrdersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<OrderDto>());

        // Act
        var response = await _controller.GetRecentOrders();

        // Assert
        response.Result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task CreateOrder_Should_Return_Created_When_OrderIsValid()
    {
        // Arrange
        var command = new CreateOrderCommand { Name = "Order1", Description = "Description1" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateOrderCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Guid());

        // Act
        var response = await _controller.CreateOrder(command);

        // Assert
        response.Result.Should().BeOfType<CreatedResult>();
    }

    [Fact]
    public async Task CreateOrder_Should_Return_BadRequest_When_ModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Name", "Name is required");

        // Act
        var response = await _controller.CreateOrder(new CreateOrderCommand());

        // Assert
        response.Result.Should().BeOfType<BadRequestObjectResult>();
    }
}
