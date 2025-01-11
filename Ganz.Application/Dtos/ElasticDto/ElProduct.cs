namespace Ganz.Application.Dtos.ElasticDto
{
    public class ElProduct
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
