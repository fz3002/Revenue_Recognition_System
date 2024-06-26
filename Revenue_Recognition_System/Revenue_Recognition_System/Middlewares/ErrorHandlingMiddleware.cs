using System.Net;
using Microsoft.Data.SqlClient;
using Revenue_Recognition_System.Exceptions;

namespace Revenue_Recognition_System.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            _logger.LogError(ex, "An unhandled DomainException occurred");

            await HandleDomainExceptionAsync(context, ex);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "An unhandled SqlException occurred");

            await HandleSqlExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled Exception occurred");

            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleDomainExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = new
            {
                message = "Error in request formating",
                detail = ex.Message
            }
        };

        var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(jsonResponse);
    }

    private Task HandleSqlExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = new
            {
                message = "Error occured within database",
                detail = ex.Message
            }
        };

        var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(jsonResponse);
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = new
            {
                message = "Error occured while processing request",
                detail = ex.Message
            }
        };

        var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(jsonResponse);
    }
}