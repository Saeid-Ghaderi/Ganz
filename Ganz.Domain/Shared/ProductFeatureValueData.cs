using Ganz.Domain.Catalogs.Features;

namespace Ganz.Domain.Shared
{
    public record class ProductFeatureValueData
    {
        public FeatureId FeatureId { get; init; }
        public string Value { get; init; }

        public ProductFeatureValueData(FeatureId featureId, string value)
        {
            Value = value;
            FeatureId = featureId;
        }
    }
}
