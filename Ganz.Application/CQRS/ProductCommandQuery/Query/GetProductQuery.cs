using AutoMapper;
using Ganz.Application.Dtos;
using Ganz.Application.Utilities;
using Ganz.Domain.Contracts;
using Ganz.Domain.Enttiies;
using Ganz.Domain.Pagination;
using MediatR;

namespace Ganz.Application.CQRS.ProductCommandQuery.Query
{
    public class GetProductQuery:IRequest<GetProductQueryResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetProductQueryResponse
    {
        public IEnumerable<Product> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var req = _mapper.Map<PaginationRequest>(request);
            var pagedResult = await _productRepository.GetProductsAsync(req);
            //Mapping By Class
            var productDtos = pagedResult.Items.Select(product => product.ToResponseDTO()).ToList();

            //var productDtos = _mapper.Map<List<ProductResponseDTO>>(pagedResult);
            var products = new PaginationResponse<ProductResponseDTO>
            {
                Items = productDtos,
                TotalCount = pagedResult.TotalCount,
                PageNumber = req.PageNumber,
                PageSize = req.PageSize
            };

            var response = _mapper.Map<GetProductQueryResponse>(products);
            return response;
        }
    }


}
