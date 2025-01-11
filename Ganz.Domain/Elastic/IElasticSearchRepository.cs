using Nest;

namespace Ganz.Domain.Elastic
{
    public interface IElasticSearchRepository<T> where T : class
    {
        Task IndexAsync(T document);
        Task<T?> GetByIdAsync(string id);
        Task<IEnumerable<T>> SearchAsync(Func<SearchDescriptor<T>, ISearchRequest> selector);
    }
}
