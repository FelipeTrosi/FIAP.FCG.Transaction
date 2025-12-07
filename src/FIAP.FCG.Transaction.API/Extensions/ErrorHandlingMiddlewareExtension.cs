using FIAP.FCG.Transaction.API.Middleware;

namespace FIAP.FCG.Transaction.API.Extensions;

public static class ErrorHandlingMiddlewareExtension
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
