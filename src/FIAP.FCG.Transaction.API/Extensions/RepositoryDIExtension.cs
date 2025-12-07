using FIAP.FCG.Transaction.Infrastructure.Repository;
using FIAP.FCG.Transaction.Infrastructure.Repository.Interfaces;

namespace FIAP.FCG.Transaction.API.Extensions
{
    public static class RepositoryDIExtension
    {
        public static IServiceCollection AddRepositoryDI(this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            return services;
        }
    }
}
