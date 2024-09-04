using MediatR;
using SampleAPI.Application.Contracts.Repositories;

namespace SampleAPI.Application.Features.Order.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Entities.Order()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            EntryDate = DateTime.UtcNow,
            IsInvoiced = true,
            IsDeleted = false
        };

        await _orderRepository.CreateOrderAsync(order);
        return order.Id;
    }
}