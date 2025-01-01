using Ganz.Application.Dtos;
using Ganz.Domain.Pagination;

namespace Ganz.Application.Interfaces
{
    public interface IProductService
    {
        Task<PaginationResponse<ProductResponseDTO>> GetProductsAsync(PaginationRequest paginationRequest);
        Task<ProductResponseDTO> GetByIdAsync(int id);
        Task AddProductAsync(ProductRequestDTO product);
        Task UpdateAsync(ProductRequestDTO product);
        Task PatchAsync(int id, Dictionary<string, object> updates);
        Task<bool> RemoveProductAsync(int id);
    }
}
