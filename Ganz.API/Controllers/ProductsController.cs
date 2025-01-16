using Ganz.API.CustomAttributes;
using Ganz.API.General;
using Ganz.Application.Dtos;
using Ganz.Application.Interfaces;
using Ganz.Application.Services;
using Ganz.Domain.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Cryptography;

namespace Ganz.API.Controllers
{

    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private IProductService _mockobject;

        public ProductsController(IProductService productService, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _productService = productService;
            _environment = environment;
            _configuration = configuration;
        }

        public ProductsController(IProductService mockobject)
        {
            _mockobject = mockobject;
        }

        [HttpPost("CreateProduct")]
        [SwaggerOperation(
          Summary = "Create a Product",
          Description = "Create a Product",
          OperationId = "Products.Create",
          Tags = new[] { "ProductController" })
        ]
        public async Task<ActionResult<ApiResponse<ProductRequestDTO>>> CreateProduct([FromBody] ProductRequestDTO product)
        {
            if (product == null)
            {
                return BadRequest(ApiResponse<ProductRequestDTO>.Failure(400, "Invalid product data"));
            }

            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(CreateProduct),
                new { id = product.Id }, ApiResponse<ProductRequestDTO>.Success(201, product, "Product created successfully"));
        }

        [Authorize]
        [HttpGet("GetProducts")]
        [AccessControl(Permission = "get-product")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductResponseDTO>))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProducts([FromQuery] PaginationRequest paginationRequest)
        {
            var result = await _productService.GetProductsAsync(paginationRequest);
            return Ok(ApiResponse<PaginationResponse<ProductResponseDTO>>.Success(200,result));
            //return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [AllowAnonymous]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] ProductRequestDTO product)
        {
            if (product == null)
                return BadRequest("Product cannot be null");

            try
            {
                await _productService.AddProductAsync(product);
                return Ok("Product added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchProductDto updates)
        {
            try
            {
                await _productService.PatchAsync(id, updates);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateProductDto dto)
        {
            try
            {
                var product = new ProductRequestDTO(dto.Name, dto.Price, dto.Description);
                await _productService.UpdateAsync(product);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var result = await _productService.RemoveProductAsync(id);

            if (result)
            {
                return Ok("Product Removed Successfully!!!");
            }
            return NotFound();
        }



        [HttpPut]
        public async Task UpdateProduct([FromBody] ProductRequestDTO product)
        {
            await _productService.UpdateProductAsync(product);
        }


        [HttpPatch("PatchProductAsync/{id}")]
        public async Task<IActionResult> PatchProductAsync(int id, [FromBody] Dictionary<string, object> updates)
        {
            if (updates == null || !updates.Any())
            {
                return BadRequest("Updates cannot be null or empty.");
            }

            try
            {
                await _productService.PatchProductAsync(id, updates);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost("upload")]
        [AllowAnonymous]
        public async Task<IActionResult> Upload(IFormFile thumbnail)
        {
            //1-save to byte[]
            using (var target = new MemoryStream())
            {
                thumbnail.CopyTo(target);
                var thumbnailByteArray = target.ToArray();
            }

            //2-save in folders
            //string filePath = @"E:\"; //???
            var wwwRootPath = _environment.WebRootPath;
            var contentRootPath = _environment.ContentRootPath;
            var mediaPath = _configuration.GetValue<string>("MediaPath");
            var productFolder = "Product";

            //check mediaPath Directory Exists
            if (!Directory.Exists(Path.Combine(wwwRootPath, mediaPath)))
            {
                Directory.CreateDirectory(Path.Combine(wwwRootPath, mediaPath));
            }

            //check productFolder Directory Exists
            if (!Directory.Exists(Path.Combine(wwwRootPath, mediaPath, productFolder)))
            {
                Directory.CreateDirectory(Path.Combine(wwwRootPath, mediaPath, productFolder));
            }

            FileInfo fileInfo = new FileInfo(thumbnail.FileName);
            string fileName = thumbnail.FileName + fileInfo.Extension;


            var uniqueId = DateTime.Now.Ticks;
            var newFileName = $"{uniqueId}{fileInfo.Extension}";

            string fileNameWithPath = Path.Combine(wwwRootPath, mediaPath, productFolder, newFileName);

            //convert IFormFile to byte[]
            byte[] encryptedData;
            using (var ms = new MemoryStream())
            {
                thumbnail.CopyTo(ms);
                var fileBytes = ms.ToArray();
                encryptedData = Encrypt(fileBytes);
            }


            //write byte[] to file
            using var writer = new BinaryWriter(System.IO.File.OpenWrite(fileNameWithPath));
            writer.Write(encryptedData);

            return Ok(fileNameWithPath);
        }

        //[HttpGet("GetFile")]
        //[AllowAnonymous]
        //public FileResult GetFile(string filePath)
        //{
        //    return File(filePath, "application/pdf");
        //}

        // [HttpGet("GetFileContent")]
        // [AllowAnonymous]
        // public async Task<FileContentResult> GetFileContent(string filePath)
        // {
        //     //read file and decrypt content
        //     byte[] encryptedData = await System.IO.File.ReadAllBytesAsync(filePath);
        //     var decryptedData = Decrypt(encryptedData);

        //     return new FileContentResult(decryptedData, "application/txt");
        // }

        private byte[] Encrypt(byte[] fileContent)
        {
            string EncryptionKey = "MAKV2SPBNI54324";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(fileContent, 0, fileContent.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        private byte[] Decrypt(byte[] fileContent)
        {
            string EncryptionKey = "MAKV2SPBNI54324";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);


                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(fileContent, 0, fileContent.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }
    }
}
