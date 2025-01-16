using AutoMapper;
using Ganz.Application.Dtos;
using Ganz.Application.Services;
using Ganz.Domain;
using Ganz.Domain.Enttiies;
using Ganz.Domain.Pagination;
using Ganz.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ganz.IntegrationTests.Services
{
    public class ProductServiceIntegrationTests
    {
        private readonly ProductService _productService;
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        public ProductServiceIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new ApplicationDBContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductResponseDTO>();
                cfg.CreateMap(typeof(PaginationResponse<>), typeof(PaginationResponse<>));
            });
            _mapper = config.CreateMapper();

            _productService = new ProductService(new ProductRepository(_dbContext, null), _mapper);

            SeedData();
        }

        private void SeedData()
        {
            _dbContext.Products.Add(new Product { Id = 1, Name = "Product 1", Price = 100, Description = "Pro1" });
            _dbContext.Products.Add(new Product { Id = 2, Name = "Product 2", Price = 200, Description = "Pro2" });
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnPaginatedProducts()
        {
            var paginationRequest = new PaginationRequest { PageNumber = 1, PageSize = 10 };
            var result = await _productService.GetProductsAsync(paginationRequest);

            // Assertions
            Assert.Equal(2, result.Items.Count());
            Assert.Equal(1, result.PageNumber);
            Assert.Equal(10, result.PageSize);
            Assert.Equal(2, result.TotalCount);
        }
    }
}
