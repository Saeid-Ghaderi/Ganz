using Ganz.Application.Services;
using Ganz.Domain.Contracts;
using Ganz.Domain.Enttiies;
using Ganz.Domain.Pagination;
using Moq;
using Shouldly;

namespace Ganz.UnitTests.Services;

public class ProductServiceUnitTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly ProductService _productService;

    public ProductServiceUnitTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _productService = new ProductService(_mockProductRepository.Object);
    }

    [Fact]
    public async Task GetProductsAsync_ShouldReturnPaginatedResponse_WhenCalled()
    {
        // Arrange
        var paginationRequest = new PaginationRequest
        {
            PageNumber = 1,
            PageSize = 2
        };

        var products = new List<Product>
    {
        new Product(1, "Product1", 100, "Description1"),
        new Product(2, "Product2", 200, "Description2"),
        new Product(3, "Product3", 300, "Description3")
    };

        var pagedResult = new PaginationResponse<Product>
        {
            Items = products.Take(2).ToList(),
            TotalCount = products.Count
        };

        _mockProductRepository
            .Setup(repo => repo.GetProductsAsync(paginationRequest))
            .ReturnsAsync(pagedResult);

        // Act
        var result = await _productService.GetProductsAsync(paginationRequest);

        // Assert

        //_mockProductRepository.Verify(repo => repo

        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count());
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(2, result.PageSize);
        Assert.Equal(3, result.TotalCount);

        //Assert With Shouldly
        //result.ShouldNotBeNull();

        _mockProductRepository.Verify(repo => repo.GetProductsAsync(paginationRequest), Times.Once);
    }
}