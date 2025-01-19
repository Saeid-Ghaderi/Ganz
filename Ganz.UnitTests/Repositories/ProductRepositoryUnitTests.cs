using Ganz.Domain;
using Ganz.Domain.Contracts;
using Ganz.Domain.Enttiies;
using Ganz.Domain.Pagination;
using Ganz.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Ganz.UnitTests.Repositories
{
    public class ProductRepositoryUnitTests
    {
        private readonly ApplicationDBContext _context;
        private readonly ProductRepository _productRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public ProductRepositoryUnitTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDBContext(options);

            _mockUnitOfWork = new Mock<IUnitOfWork>();

            SeedDatabase(_context);

            _productRepository = new ProductRepository(_context, _mockUnitOfWork.Object);
        }

        private void ClearDatabase(ApplicationDBContext context)
        {
            context.Products.RemoveRange(context.Products);
            context.SaveChanges();
        }

        private void SeedDatabase(ApplicationDBContext context)
        {

            ClearDatabase(context);
            context.Products.AddRange(
            new Product(1, "Product1", 100, "Pro1"),
            new Product(2, "Product2", 200, "Pro2"),
            new Product(3, "Product3", 300, "Pro3")
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
                PageSize = 3
            };

            // Act
            var result = await _productRepository.GetProductsAsync(paginationRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Items.Count());
            Assert.Equal(3, result.TotalCount);
            Assert.Equal("Product1", result.Items.OrderBy(p => p.Id).ElementAt(0).Name);
            Assert.Equal("Product2", result.Items.OrderBy(p => p.Id).ElementAt(1).Name);
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnEmptyResult_WhenPageOutOfBounds()
        {
            // Arrange
            var paginationRequest = new PaginationRequest
            {
                PageNumber = 0,
                PageSize = 0
            };

            // Act
            var result = await _productRepository.GetProductsAsync(paginationRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Items);
            Assert.True(result.TotalPages < 0);
        }

    //    [Fact]
    //    public async Task GetProductsAsync_ShouldReturnPaginatedResults()
    //    {
    //        var options = new DbContextOptionsBuilder<ApplicationDBContext>()
    //    .UseInMemoryDatabase(databaseName: "TestDatabase")
    //    .Options;

    //        // Arrange
    //        var data = new List<Product>
    //{
    //    new Product(1, "Product1", 100, "Pro1"),
    //    new Product(2, "Product2", 200, "Pro2")
    //}.AsQueryable();

    //        var mockDbSet = new Mock<DbSet<Product>>();

    //        // Setup IQueryable behavior for DbSet
    //        mockDbSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
    //        mockDbSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
    //        mockDbSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
    //        mockDbSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

    //        var mockContext = new Mock<ApplicationDBContext>(options);
    //        mockContext.Setup(c => c.Products).Returns(mockDbSet.Object);

    //        var mockUnitOfWork = new Mock<IUnitOfWork>();
    //        var repository = new ProductRepository(mockContext.Object, mockUnitOfWork.Object);
    //        var request = new PaginationRequest { PageNumber = 1, PageSize = 1 };

    //        // Act
    //        var result = await repository.GetProductsAsync(request);

    //        // Assert
    //        Assert.NotNull(result);
    //        Assert.Single(result.Items);
    //        Assert.Equal(1, result.PageNumber);
    //        Assert.Equal(1, result.PageSize);
    //        Assert.Equal(2, result.TotalCount);
    //    }
    }
}
