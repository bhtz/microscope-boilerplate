#if (Aspire)
using Microscope.Boilerplate.Clients.Doc.Endpoints;
using Microscope.Boilerplate.Clients.Doc.Extensions;
#endif

var builder = WebApplication.CreateBuilder(args);

#if (Aspire)
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
#endif

var app = builder.Build();

#if (Aspire)
app.UseAuthentication();
app.UseAuthorization();
app.MapAuthenticationEndpoints();
app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "no-store");

        if (ctx.Context.User.Identity is { IsAuthenticated: true }) return;
            
        // respond HTTP 401 Unauthorized with empty body.
        // ctx.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        // ctx.Context.Response.ContentLength = 0;
        // ctx.Context.Response.Body = Stream.Null;

        // - or, redirect to another page. -
        ctx.Context.Response.Redirect("/auth/login");
    }
});
#endif

#if (!Aspire)
app.UseFileServer();
#endif

app.Run();
