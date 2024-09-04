using Microsoft.EntityFrameworkCore;
using SampleAPI.Application.Contracts.Repositories;
using SampleAPI.Infrastructure.Data;
using SampleAPI.Infrastructure.Repositories;

namespace SampleAPI.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<SampleApiDbContext>(options => options.UseInMemoryDatabase(databaseName: "SampleDB"));
        services.AddRepositories();
        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IOrderRepository, OrderRepository>();
    }
}