using System.Net;
using FluentValidation;
using Microscope.Boilerplate.Services.TodoApp.Application.Common.Exceptions;
using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Api.Middlewares;

public class HttpExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public HttpExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this._next.Invoke(context);
        }
        catch (ConflictException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (PoliciesException ex)
        {   
            context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (ValidationException ex)
        {   
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (UnauthorizedException ex)
        {   
            context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync(ex.Message);
        }
    }
}
