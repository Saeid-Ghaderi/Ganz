using Microsoft.AspNetCore.Http;

namespace Ganz.Application.Dtos
{
    public class ProductRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public IFormFile Thumbnail { get; set; }
        public string ThumbnailBase64 { get; set; }
        public string ThumbnailUrl { get; set; }

        public ProductRequestDTO()
        {
            
        }
        public ProductRequestDTO(string name, decimal price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }

        public void Update(string name, decimal price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }

        public void UpdatePrice(decimal price)
        {
            Price = price;
        }
    }
}
