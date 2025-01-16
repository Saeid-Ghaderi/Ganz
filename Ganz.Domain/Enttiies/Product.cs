namespace Ganz.Domain.Enttiies
{
    public class Product
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public decimal Price { get;  set; }
        public string Description { get;  set; }
        public bool IsActive { get; set; } = true;
        public byte[] Thumbnail { get; set; } = Array.Empty<byte>();
        public string? ThumbnailFileName { get; set; } = "";
        public long ThumbnailFileSize { get; set; } = 0;
        public string? ThumbnailFileExtenstion { get; set; } = "";

        public Product()
        {
            
        }

        public Product(int id, string name, decimal price, string description)
        {
            Id = id;
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

        public void UpdateFromDictionary(Dictionary<string, object> updates)
        {
            foreach (var update in updates)
            {
                switch (update.Key.ToLower())
                {
                    case "name":
                        Name = update.Value.ToString()!;
                        break;
                    case "price":
                        Price = Convert.ToInt32(update.Value);
                        break;
                    case "description":
                        Description = update.Value.ToString()!;
                        break;
                    default:
                        throw new ArgumentException($"Property '{update.Key}' is not valid for patching.");
                }
            }
        }
    }
}
