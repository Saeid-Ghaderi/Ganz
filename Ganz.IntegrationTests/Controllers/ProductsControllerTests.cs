using Ganz.Application.Dtos;
using Ganz.Domain.Pagination;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace Ganz.IntegrationTests.Controllers
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductsControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetProducts_ShouldReturnPagedResult_WhenCalled()
        {
            // Arrange
            var paginationRequest = new PaginationRequest { PageNumber = 1, PageSize = 1 };
            var queryString = $"?Page={paginationRequest.PageNumber}&PageSize={paginationRequest.PageSize}";

            // Act
            var response = await _client.GetAsync($"/api/Products/GetProducts{queryString}");

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<PaginationResponse<ProductResponseDTO>>();
            Assert.NotNull(result);
            Assert.Equal(2, result.TotalCount);
        }
    }
}
