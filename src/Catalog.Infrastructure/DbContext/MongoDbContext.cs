using Catalog.Data.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.DbContext
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<Product> Products { get; }

        public MongoDbContext(string connectionString, string databaseName, string collection)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
            Products = _database.GetCollection<Product>(collection);
            CatalogContextSeed.SeedData(Products);
        }
    }
}