using Ganz.Domain;
using Ganz.Domain.Enttiies;
using Ganz.Domain.Pagination;
using Ganz.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ganz.IntegrationTests.Repositories;

public class ProductRepositoryIntegrationTests
{
    private readonly ApplicationDBContext _context;
    private readonly ProductRepository _productRepository;

    public ProductRepositoryIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDBContext(options);

        SeedDatabase(_context);

        _productRepository = new ProductRepository(_context, null);
    }

    private void SeedDatabase(ApplicationDBContext context)
    {
        context.Products.AddRange(
            new Product(1, "Product1", 100, "Description1"),
            new Product(2, "Product2", 200, "Description2"),
            new Product(3, "Product3", 300, "Description3")
        );
        context.SaveChanges();
    }

    [Fact]
    public async Task GetProductsAsync_ShouldReturnPagedResult_WhenDataExists()
    {
        // Arrange
        var paginationRequest = new PaginationRequest
        {
            PageNumber = 1,
            PageSize = 2
        };

        // Act
        var result = await _productRepository.GetProductsAsync(paginationRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count());
        Assert.Equal(3, result.TotalCount);
        Assert.Equal("Product1", result.Items.ElementAt(0).Name);
        Assert.Equal("Product2", result.Items.ElementAt(1).Name);
    }

    [Fact]
    public async Task GetProductsAsync_ShouldReturnEmptyResult_WhenPageOutOfBounds()
    {
        // Arrange
        var paginationRequest = new PaginationRequest
        {
            PageNumber = 10,
            PageSize = 2
        };

        // Act
        var result = await _productRepository.GetProductsAsync(paginationRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(3, result.TotalCount);
    }
}
