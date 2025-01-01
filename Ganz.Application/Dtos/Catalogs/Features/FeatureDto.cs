namespace Ganz.Application.Dtos.Catalogs.Features
{
    public class FeatureDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
    }
}
