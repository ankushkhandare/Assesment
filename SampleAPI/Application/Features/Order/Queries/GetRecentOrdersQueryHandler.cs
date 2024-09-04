using AutoMapper;
using MediatR;
using SampleAPI.Application.Contracts.Repositories;

namespace SampleAPI.Application.Features.Order.Queries;

public class GetRecentOrdersQueryHandler: IRequestHandler<GetRecentOrdersQuery, List<OrderDto>>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    
    public GetRecentOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public async Task<List<OrderDto>> Handle(GetRecentOrdersQuery request, CancellationToken cancellationToken)
    {
        var recentOrders = await _orderRepository.GetRecentOrdersAsync();
        var data =_mapper.Map<List<OrderDto>>(recentOrders);

        return data;
    }
}