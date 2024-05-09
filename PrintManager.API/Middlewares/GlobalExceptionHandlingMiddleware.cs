using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace PrintManager.API.Middlewares;

/// <summary>
/// Middleware для обработки глобальных исключений и возврата соответствующего ответа клиенту.
/// </summary>
public class GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) : IMiddleware
{
    /// <summary>
    /// Обрабатывает запрос и вызывает следующий делегат запроса в цепочке middleware.
    /// </summary>
    /// <param name="context">Объект HttpContext, представляющий текущий запрос и ответ.</param>
    /// <param name="next">Следующий делегат запроса в цепочке middleware.</param>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);

            context.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;

            ProblemDetails problemDetails = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = ex.Source,
                Title = "An internal server has occurred",
                Detail = ex.Message,
            };

            var json = JsonSerializer.Serialize(problemDetails);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}
