var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapReverseProxy();
app.UseStaticFiles();
app.UseBlazorFrameworkFiles();
app.MapFallbackToFile("index.html");

app.Run();