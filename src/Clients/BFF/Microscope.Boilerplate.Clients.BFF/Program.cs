using Microscope.Boilerplate.Clients.BFF.Configurations;

var builder = WebApplication.CreateBuilder(args);

// register reverse proxy configuration
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// builder.Services.AddSimpleGraphQlGateway(builder.Configuration);
builder.Services.AddGraphQlGateway(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseHsts();

// expose reverse proxy
app.MapReverseProxy();

// expose grapqhql gateway
app.MapGraphQL();

// expose PWA Blazor app
app.UseStaticFiles();
app.UseBlazorFrameworkFiles();
app.MapFallbackToFile("index.html");

app.Run();