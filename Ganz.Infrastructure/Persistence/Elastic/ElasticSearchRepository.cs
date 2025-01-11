using Ganz.Domain.Elastic;
using Nest;

namespace Ganz.Infrastructure.Persistence.Elastic
{
    public class ElasticSearchRepository<T> : IElasticSearchRepository<T> where T : class
    {
        private readonly IElasticClient _elasticClient;

        public ElasticSearchRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task IndexAsync(T document)
        {
            var response = await _elasticClient.IndexDocumentAsync(document);
            if (!response.IsValid)
                throw new Exception(response.OriginalException.Message);
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            var response = await _elasticClient.GetAsync<T>(id);
            return response.Source;
        }

        public async Task<IEnumerable<T>> SearchAsync(Func<SearchDescriptor<T>, ISearchRequest> selector)
        {
            var response = await _elasticClient.SearchAsync<T>(selector);
            if (!response.IsValid)
                throw new Exception(response.OriginalException.Message);

            return response.Documents;
        }
    }
}
