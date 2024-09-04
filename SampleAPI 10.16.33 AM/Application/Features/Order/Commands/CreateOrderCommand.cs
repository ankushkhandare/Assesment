using MediatR;

namespace SampleAPI.Application.Features.Order.Commands;

public class CreateOrderCommand: IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}