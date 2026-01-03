using FIAP.FCG.Transaction.Infrastructure.Logger;
using FIAP.FCG.Transaction.Service.Interfaces.Services;
using FIAP.FCG.Transaction.Service.Services;

namespace FIAP.FCG.Transaction.API.Extensions; 

public static class ServiceDIExtension
{
    public static IServiceCollection AddServiceDI(this IServiceCollection services)
    {
        services.AddTransient(typeof(IBaseLogger<>), typeof(BaseLogger<>));
        services.AddTransient<ITransactionService, TransactionService>();
        services.AddTransient<IPaymentService, PaymentService>();

        return services;
    }
}
