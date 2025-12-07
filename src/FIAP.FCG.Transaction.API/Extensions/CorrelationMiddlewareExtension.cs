using FIAP.FCG.Transaction.Infrastructure.Middlewares;

namespace FIAP.FCG.Transaction.API.Extensions;

public static class CorrelationMiddlewareExtension
{
    public static IApplicationBuilder UseCorrelationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CorrelationMiddleware>();
    }
}
