using Ganz.Domain.Enttiies;
using Ganz.Domain.Pagination;

namespace Ganz.Domain.Contracts
{
    public interface IProductRepository
    {
        Task<PaginationResponse<Product>> GetProductsAsync(PaginationRequest paginationRequest);
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<int> InsertAsync(Product product);
        Task UpdateAsync(Product product); // PUT
        Task PatchAsync(int id, Dictionary<string, object> updates); // PATCH
        Task DeleteAsync(int id);

        Task PatchProductAsync(int id, Dictionary<string, object> updates);
        Task UpdateProductAsync(Product product);

    }
}
