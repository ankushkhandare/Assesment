using AutoMapper;
using SampleAPI.Application.Features.Order.Commands;
using SampleAPI.Application.Features.Order.Queries;
using SampleAPI.Entities;

namespace SampleAPI.Application.MappingProfiles;

public class OrderProfile: Profile
{
    public OrderProfile()
    {
        CreateMap<OrderDto, Order>().ReverseMap();
        CreateMap<CreateOrderCommand, Order>();
    }
}