using Ganz.Application.Dtos;
using Ganz.Domain.Enttiies;

namespace Ganz.Application.Utilities
{
    public static class ProductMapper
    {
        public static ProductResponseDTO ToResponseDTO(this Product product)
        {
            return new ProductResponseDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
        }

        
        public static Product ToDomainEntity(this ProductRequestDTO productRequest)
        {
            return new Product(productRequest.Id, productRequest.Name,productRequest.Price,productRequest.Description);
        }
    }
}
