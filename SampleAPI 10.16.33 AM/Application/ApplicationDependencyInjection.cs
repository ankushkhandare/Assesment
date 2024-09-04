using FluentValidation;
using SampleAPI.Application.Features.Order.Commands;
using SampleAPI.Application.Features.Order.Queries;
using SampleAPI.Application.MappingProfiles;

namespace SampleAPI.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetRecentOrdersQuery>());
        services.AddAutoMapper(typeof(OrderProfile));
        services.AddTransient<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
        return services;
    }
}