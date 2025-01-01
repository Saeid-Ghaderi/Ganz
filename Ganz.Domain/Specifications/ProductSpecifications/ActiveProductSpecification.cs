using Ganz.Domain.Enttiies;
using System.Linq.Expressions;

namespace Ganz.Domain.Specifications.ProductSpecifications
{
    public class ActiveProductSpecification : ISpecification<Product>
    {
        public Expression<Func<Product, bool>> Criteria => p => p.IsActive;

        public List<Expression<Func<Product, object>>> Includes => new List<Expression<Func<Product, object>>>();
    }
}
