using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new
{
  name = "APIGateway",
  type = "ocelot",
  status = "ok"
}));

await app.UseOcelot();

app.Run();

