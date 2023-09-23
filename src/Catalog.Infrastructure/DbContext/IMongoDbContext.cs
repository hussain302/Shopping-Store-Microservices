using Catalog.Data.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.DbContext
{
    public interface IMongoDbContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
