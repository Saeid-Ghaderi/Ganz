using Ganz.Domain.Base;

namespace Ganz.Domain.Catalogs.Products
{
    public sealed class ProductId : StronglyTypedId<ProductId>
    {
        public ProductId(Guid value) : base(value)
        {

        }
    }
}
