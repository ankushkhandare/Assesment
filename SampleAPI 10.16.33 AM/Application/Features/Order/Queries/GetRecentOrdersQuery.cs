using MediatR;

namespace SampleAPI.Application.Features.Order.Queries;

public class GetRecentOrdersQuery : IRequest<List<OrderDto>>
{
    
}