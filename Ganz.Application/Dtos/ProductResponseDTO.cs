using Microsoft.AspNetCore.Http;

namespace Ganz.Application.Dtos
{
    public class ProductResponseDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public IFormFile Thumbnail { get; set; }
        public string? ThumbnailBase64 { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
