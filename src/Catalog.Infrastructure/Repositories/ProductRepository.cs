using Catalog.Data.Entities;
using Catalog.Infrastructure.DbContext;
using Catalog.Infrastructure.IRepositories;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IBaseRepository<Product>
    {
        private readonly IMongoDbContext _dbContext;
        public ProductRepository(IMongoDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                return await _dbContext
                                       .Products
                                       .Find(_ => true)
                                       .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Product> GetByIdAsync(string id)
        {
            try
            {
                return await _dbContext
                                       .Products
                                       .Find(p => p.Id == id)
                                       .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> filterExpression)
        {
            try
            {
                return await _dbContext
                                       .Products
                                       .Find(filterExpression)
                                       .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> InsertAsync(Product entity)
        {
            try
            {
                await _dbContext.Products.InsertOneAsync(entity);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateAsync(string id, Product entity)
        {
            try
            {
                var result = await _dbContext.Products.ReplaceOneAsync(p => p.Id == id, entity);
                return result.IsAcknowledged && result.ModifiedCount > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var result = await _dbContext.Products.DeleteOneAsync(p => p.Id == id);
                return result.IsAcknowledged && result.DeletedCount > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}