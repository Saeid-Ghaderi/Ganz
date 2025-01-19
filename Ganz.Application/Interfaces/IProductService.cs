using Ganz.Application.Dtos;
using Ganz.Domain.Enttiies;
using Ganz.Domain.Pagination;

namespace Ganz.Application.Interfaces
{
    public interface IProductService
    {
        Task<PaginationResponse<ProductResponseDTO>> GetProductsAsync(PaginationRequest paginationRequest);
        Task<ProductResponseDTO> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductRequestDTO product);
        Task UpdateAsync(ProductRequestDTO product);
        Task PatchAsync(int id, Dictionary<string, object> updates);
        Task<bool> RemoveProductAsync(int id);

        Task UpdateProductAsync(ProductRequestDTO productrequest);
        Task PatchProductAsync(int id, Dictionary<string, object> updates);
    }
}
