using Ganz.Application.Services;
using Ganz.Domain.Contracts;
using Ganz.Domain.Enttiies;
using Moq;

namespace Ganz.Application.Tests
{
    public class UnitTest1
    {
        public class ProductServiceTests
        {
            //[Fact]
            //public async Task AddProductAsync_ShouldAddProduct_WhenValidProductIsProvided()
            //{
            //    // Arrange
            //    var mockUnitOfWork = new Mock<IUnitOfWork>();
            //    var mockRepository = new Mock<IProductRepository<Product>>();
            //    var productService = new ProductService(mockUnitOfWork.Object);

            //    var product = new Product
            //    {
            //        ID = Guid.NewGuid(),
            //        Name = "Test Product",
            //        Price = 100
            //    };

            //    mockUnitOfWork.Setup(uow => uow.ProductRepository).Returns(mockRepository.Object);

            //    // Act
            //    await productService.AddProductAsync(product);

            //    // Assert
            //    mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
            //    mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            //}

            //[Fact]
            //public async Task AddProductAsync_ShouldThrowException_WhenProductIsNull()
            //{
            //    // Arrange
            //    var mockUnitOfWork = new Mock<IUnitOfWork>();
            //    var productService = new ProductService(mockUnitOfWork.Object);

            //    // Act & Assert
            //    await Assert.ThrowsAsync<ArgumentNullException>(() => productService.AddProductAsync(null!));
            //}
        }

    }
}