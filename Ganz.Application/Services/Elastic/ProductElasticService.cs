using Ganz.Application.Dtos;
using Ganz.Application.Dtos.ElasticDto;
using Ganz.Domain.Elastic;

namespace Ganz.Application.Services.Elastic
{
    public class ProductElasticService
    {
        private readonly IElasticSearchRepository<ElProduct> _repository;

        public ProductElasticService(IElasticSearchRepository<ElProduct> repository)
        {
            _repository = repository;
        }

        public async Task AddProductAsync(ElProduct product)
        {
            await _repository.IndexAsync(product);
        }

        public async Task<ElProduct?> GetProductByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ElProduct>> SearchProductsAsync(string query)
        {
            //return await _repository.SearchAsync(s => s
            //    .Query(q => q
            //        .Match(m => m
            //            .Field(f => f.Name)
            //            .Query(query))));


            return await _repository.SearchAsync(s => s
            .Query(q => q
                .Prefix(p => p
                    .Field(f => f.Name)
                    .Value(query.ToLower()))));


            //        return await _repository.SearchAsync(s => s
            //.Query(q => q
            //    .Wildcard(w => w
            //        .Field(f => f.Name)
            //        .Value($"*{query.ToLower()}*"))));
        }
    }

}
