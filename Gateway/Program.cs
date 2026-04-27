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

app.Use(async (context, next) =>
{
  await next();

  if (context.Response.HasStarted)
  {
    return;
  }

  if (context.Response.StatusCode != StatusCodes.Status404NotFound)
  {
    return;
  }

  context.Response.ContentType = "application/json";
  await context.Response.WriteAsJsonAsync(new
  {
    error = "No matching route found",
    path = context.Request.Path.Value,
    method = context.Request.Method
  });
});

await app.UseOcelot();

app.Run();