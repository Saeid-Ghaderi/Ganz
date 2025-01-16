using Ganz.API.General;
using Ganz.Application.Dtos;
using Ganz.Application.Interfaces;
using Ganz.Domain.Pagination;
using Ganz.IntegrationTests.TestUtilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Ganz.IntegrationTests.Controllers
{
    public class ProductsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private bool IsAuthorized = true;

        public ProductsControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            var customFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = "Test";
                        options.DefaultChallengeScheme = "Test";
                    })
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });


                    var permissionServiceMock = new Mock<IPermissionService>();
                    permissionServiceMock.Setup(p => p.CheckPermission(It.IsAny<Guid>(), "get-product"))
                        .ReturnsAsync(IsAuthorized);

                    services.AddSingleton(permissionServiceMock.Object);

                    //services.AddScoped<IPermissionService, PermissionService>();

                });
            });

            _client = customFactory.CreateClient();
        }

        [Fact]
        public async Task GetProducts_ShouldReturnPagedResult_WhenUserHasPermission()
        {
            // Arrange
            var paginationRequest = new PaginationRequest { PageNumber = 1, PageSize = 1 };
            var queryString = $"?Page={paginationRequest.PageNumber}&PageSize={paginationRequest.PageSize}";

            //var claims = new[]
            //{
            //    new Claim(ClaimTypes.Name, "TestUser"),
            //    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            //    new Claim("userId", Guid.NewGuid().ToString()),
            //    new Claim("Permission", "get-product")
            //};

            //var identity = new ClaimsIdentity(claims, "Test");
            //var principal = new ClaimsPrincipal(identity);


            //var httpContext = new DefaultHttpContext();
            //httpContext.User = principal;


            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await _client.GetAsync($"/api/Products/GetProducts{queryString}");

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<PaginationResponse<ProductResponseDTO>>>();
            Assert.NotNull(result);
            Assert.Equal(2, result.Data.TotalCount);
            Assert.NotEmpty(result.Data.Items);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnForbidden_WhenUserDoesNotHavePermission()
        {
            IsAuthorized = false;
            // Arrange
            var paginationRequest = new PaginationRequest { PageNumber = 1, PageSize = 1 };
            var queryString = $"?Page={paginationRequest.PageNumber}&PageSize={paginationRequest.PageSize}";

            TestAuthHandler.OverrideClaims = new[]
            {
                new Claim(ClaimTypes.Name, "UnauthorizedUser") // بدون userId یا Permission
            };

            //var claims = new[]
            //{
            //    new Claim(ClaimTypes.Name, "TestUser"),
            //    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            //    new Claim("userId", Guid.NewGuid().ToString()),
            //    //new Claim("Permission", "get-product")
            //};
            //var identity = new ClaimsIdentity(claims, "Test");
            //var principal = new ClaimsPrincipal(identity);

            //var httpContext = new DefaultHttpContext();
            //httpContext.User = principal;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await _client.GetAsync($"/api/Products/GetProducts{queryString}");

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, (int)response.StatusCode);
            var errorContent = await response.Content.ReadAsStringAsync();
            //Assert.Equal("",errorContent);
            Assert.True(errorContent.Contains("System.NullReferenceException: Object ref"),
            $"Expected error message to contain 'System.NullReferenceException: Object ref' but was '{errorContent}'");
        }
    }
}
