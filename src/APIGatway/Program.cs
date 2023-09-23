using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
//builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot();
var app = builder.Build();

await app.UseOcelot();
app.MapGet("/ApiGateway", () => "Welcome to API Gateway for all Microservices");
app.Run();
