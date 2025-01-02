using Ganz.Domain.Base;

namespace Ganz.Domain.Catalogs.Features
{
    public sealed class FeatureId : StronglyTypedId<FeatureId>
    {
        public FeatureId(Guid value) : base(value)
        {

        }
    }
}
