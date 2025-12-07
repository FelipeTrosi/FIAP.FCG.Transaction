using FIAP.FCG.Transaction.API.Middleware;

namespace FIAP.FCG.Transaction.API.Extensions;

public static class LogMiddlewareExtensions
{
    public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        => builder.UseMiddleware<LogMiddleware>();

}