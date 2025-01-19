using Ganz.Domain;
using Ganz.Domain.Contracts;
using Ganz.Domain.Enttiies;
using Ganz.Domain.Pagination;
using Ganz.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using Nest;
using System.Text.Json;


namespace Ganz.Infrastructure.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IUnitOfWork _unitofwork;
        public ProductRepository(ApplicationDBContext dbContext, IUnitOfWork unitofwork)
        {
            _dbContext = dbContext;
            _unitofwork = unitofwork;
        }
        public async Task<PaginationResponse<Product>> GetProductsAsync(PaginationRequest paginationRequest)
        {
            var totalCount = await _dbContext.Products.CountAsync();

            var items = await _dbContext.Products
                .Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .ToListAsync();

            return new PaginationResponse<Product>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            };
        }
        public async Task<IEnumerable<Product>> GetBySpecificationAsync(ISpecification<Product> specification)
        {
            var query = _dbContext.Set<Product>().Where(specification.Criteria);

            foreach (var include in specification.Includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }
        public async Task<Product> GetByIdAsync(int ID)
        {
            var product = await _dbContext.Products.FindAsync(ID);

            return product ?? throw new KeyNotFoundException("Product not found");
        }
        public async Task<int> InsertAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var insertedproduct = await _dbContext.Products.AddAsync(product);
            await _unitofwork.SaveChangesAsync();

            return product.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);

            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _unitofwork.SaveChangesAsync();
            }

        }


        //In Entity
        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await _dbContext.Products.FindAsync(product.Id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {product.Id} not found.");
            }

            _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);

            await _unitofwork.SaveChangesAsync();
        }
        public async Task PatchAsync(int id, Dictionary<string, object> updates)
        {
            var existingProduct = await _dbContext.Products.FindAsync(id);
            if (existingProduct == null) throw new KeyNotFoundException($"Product with ID {id} not found.");


            foreach (var update in updates)
            {
                var property = _dbContext.Entry(existingProduct).Property(update.Key);
                if (property != null && property.IsModified == false)
                {
                    property.CurrentValue = update.Value;
                }
                else
                {
                    throw new ArgumentException($"Property '{update.Key}' not found on Product entity.");
                }
            }

            await _unitofwork.SaveChangesAsync();
        }


        //In Repository
        public async Task UpdateProductAsync(Product product)
        {
            var existingproduct = await _dbContext.Products.FindAsync(product.Id);

            if (existingproduct == null) throw new KeyNotFoundException($"{product.Id} is not found");

            _dbContext.Entry(existingproduct).CurrentValues.SetValues(product);

            await _unitofwork.SaveChangesAsync();
        }
        public async Task PatchProductAsync(int id, Dictionary<string, object> updates)
        {
            var exisitingproduct = await _dbContext.Products.FindAsync(id);
            if (exisitingproduct == null) throw new ArgumentNullException($"{id} Not Found");


            foreach (var update in updates)
            {
                var property = _dbContext.Entry(exisitingproduct).Property(update.Key);

                if (property != null && property.IsModified == false)
                {
                    object value;
                    if (update.Value is JsonElement jsonElement)
                    {
                        if (property.Metadata.ClrType == typeof(string))
                        {
                            value = jsonElement.GetString();
                        }
                        else if (property.Metadata.ClrType == typeof(int))
                        {
                            value = jsonElement.GetInt32();
                        }
                        else if (property.Metadata.ClrType == typeof(bool))
                        {
                            value = jsonElement.GetBoolean();
                        }
                        else if (property.Metadata.ClrType == typeof(decimal))
                        {
                            value = jsonElement.GetDecimal();
                        }
                        else if (property.Metadata.ClrType == typeof(DateTime))
                        {
                            value = jsonElement.GetDateTime();
                        }
                        else
                        {
                            throw new InvalidCastException($"Unsupported property type: {property.Metadata.ClrType}");
                        }
                    }
                    else
                    {
                        value = update.Value;
                    }

                    property.CurrentValue = value;
                }
                else
                {
                    throw new ArgumentException($"Property '{update.Key}' not found on Product entity.");
                }
            }


            await _unitofwork.SaveChangesAsync();
        }


    }
}
