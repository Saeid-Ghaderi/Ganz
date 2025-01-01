using Ganz.Application.Dtos;
using Ganz.Application.Interfaces;
using Ganz.Domain.Pagination;

namespace Ganz.Application.Services
{
    public class ProductServiceLoggingDecorator : IProductService
    {
        private readonly IProductService _inner;

        public ProductServiceLoggingDecorator(IProductService inner)
        {
            _inner = inner;
        }

        public void AddProduct(ProductRequestDTO product)
        {
            Console.WriteLine("Before AddProduct");
            _inner.AddProductAsync(product);
            Console.WriteLine("After AddProduct");
        }

        public Task AddProductAsync(ProductRequestDTO product)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponseDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductResponseDTO> GetProductById(int id)
        {
            Console.WriteLine("Before GetProductById");
            var product = await _inner.GetByIdAsync(id);
            Console.WriteLine("After GetProductById");
            return product;
        }

        public Task<PaginationResponse<ProductResponseDTO>> GetProductsAsync(PaginationRequest paginationRequest)
        {
            throw new NotImplementedException();
        }

        public Task PatchAsync(int id, Dictionary<string, object> updates)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ProductRequestDTO product)
        {
            throw new NotImplementedException();
        }
    }

}
