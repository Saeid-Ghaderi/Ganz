using Ganz.API.Controllers;
using Ganz.Application.Dtos;
using Ganz.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Ganz.UnitTests.Controllers
{
    public class ProductControllerUnitTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductsController _controller;

        private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private readonly Mock<IConfiguration> _mockConfiguration;

        public ProductControllerUnitTests()
        {
            _mockProductService = new Mock<IProductService>();
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _mockConfiguration = new Mock<IConfiguration>();
            _controller = new ProductsController(_mockProductService.Object,
                _mockWebHostEnvironment.Object, _mockConfiguration.Object);
        }

        [Fact]
        public async Task GetProductById_ReturnsOkResult()
        {
            // Arrange
            int productId = 1;
            var product = new ProductResponseDTO { Id = productId, Name = "Test Product" };
            _mockProductService.Setup(service => service.GetProductByIdAsync(productId))
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
            _mockProductService.Setup(service => service.GetProductByIdAsync(productId))
                .ReturnsAsync((ProductResponseDTO)(null));

            // Act
            var result = await _controller.GetProductById(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
