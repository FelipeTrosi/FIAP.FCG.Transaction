using FIAP.FCG.Transaction.Infrastructure.CorrelationId;

namespace FIAP.FCG.Transaction.API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCorrelationIdGenerator(this IServiceCollection services)
    {
        services.AddTransient<ICorrelationIdGenerator, CorrelationIdGenerator>();

        return services;
    }
}
