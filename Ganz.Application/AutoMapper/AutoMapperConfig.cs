using AutoMapper;
using Ganz.Application.CQRS.ProductCommandQuery.Query;
using Ganz.Application.Dtos;
using Ganz.Domain.Enttiies;
using Ganz.Domain.Pagination;

namespace Ganz.Application.AutoMapper
{
    public class AutoMapperConfig:Profile
    {

        public AutoMapperConfig()
        {
            CreateMap<ProductRequestDTO, Product>().ReverseMap();    
            CreateMap<ProductResponseDTO, Product>().ReverseMap();    
            CreateMap<List<ProductResponseDTO>,Product>().ReverseMap();    
            CreateMap<ProductRequestDTO,Product>().ReverseMap();   
            CreateMap<List<Product>, PaginationResponse<Product>>().ReverseMap();   
            

            CreateMap<GetProductQueryResponse,PaginationResponse<ProductResponseDTO>>().ReverseMap();
            CreateMap<GetProductQuery,PaginationRequest>().ReverseMap();
        }
    }
}
