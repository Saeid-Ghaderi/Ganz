using Ganz.API.Controllers;
using Ganz.Application.Dtos;
using Ganz.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Ganz.UnitTests.Controllers
{
    public class ProductControllerUnitTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductsController _controller;

        public ProductControllerUnitTests()
        {
            _mockProductService = new Mock<IProductService>();

            _controller = new ProductsController(_mockProductService.Object);
        }

        [Fact]
        public async Task GetProductById_ReturnsOkResult()
        {
            // Arrange
            int productId = 1;
            var product = new ProductResponseDTO { Id = productId, Name = "Test Product" };
            _mockProductService.Setup(service => service.GetByIdAsync(productId))
                .ReturnsAsync(product);

            // Act
            var result = await _controller.GetProductById(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<ProductResponseDTO>(okResult.Value);
            Assert.Equal(productId, returnedProduct.Id);
        }

        [Fact]
        public async Task GetProductById_ReturnsNotFoundResult_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 9999;
            var product = new ProductResponseDTO { Id = productId, Name = "Test Product" };
            _mockProductService.Setup(service => service.GetByIdAsync(productId))
                .ReturnsAsync(product);

            // Act
            var result = await _controller.GetProductById(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
