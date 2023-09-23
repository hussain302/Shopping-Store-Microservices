using Catalog.Data.Entities;
using Catalog.Infrastructure.DbContext;
using Catalog.Infrastructure.IRepositories;
using Catalog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//Creating API version
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

//mongoDb Connection using dependency injection
builder.Services.AddScoped<IMongoDbContext>
(
    provider => new MongoDbContext(
    connectionString: builder.Configuration.GetSection("DatabaseSettings:ConnectionString").Value    ?? string.Empty,
    databaseName:     builder.Configuration.GetSection("DatabaseSettings:DatabaseName").Value        ?? string.Empty,
    collection:       builder.Configuration.GetSection("DatabaseSettings:CollectionName").Value      ?? string.Empty
));

//Product/Catalog repositories
builder.Services.AddScoped<IBaseRepository<Product>, ProductRepository>();

//using OpenAPI Support of Swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();