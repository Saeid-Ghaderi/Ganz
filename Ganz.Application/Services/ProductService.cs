using AutoMapper;
using Azure.Core;
using FluentValidation;
using FluentValidation.Results;
using Ganz.Application.Dtos;
using Ganz.Application.Interfaces;
using Ganz.Application.Utilities;
using Ganz.Domain.Contracts;
using Ganz.Domain.Enttiies;
using Ganz.Domain.Pagination;

namespace Ganz.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductRequestDTO> _validator;
        private readonly IMyFileUtility _myFileUtility;

        public ProductService(IProductRepository productRepository, IMapper mapper, IValidator<ProductRequestDTO> validator, IMyFileUtility myFileUtility)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _validator = validator;
            _myFileUtility = myFileUtility;
        }

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductService(IProductRepository productRepository, IMapper mapper) : this(productRepository)
        {
            _mapper = mapper;
        }

        public async Task<PaginationResponse<ProductResponseDTO>> GetProductsAsync(PaginationRequest paginationRequest)
        {

            var pagedResult = await _productRepository.GetProductsAsync(paginationRequest);

            //Mapping By Class
            var productDtos = pagedResult.Items.Select(product => product.ToResponseDTO()).ToList();

            //By AutoMapper
            //var productDtos =  _mapper.Map<List<ProductResponseDTO>>(pagedResult);

            var result = new PaginationResponse<ProductResponseDTO>
            {
                Items = productDtos,
                TotalCount = pagedResult.TotalCount,
                PageNumber = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            };

            return result;

        }

        public Task<ProductResponseDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task AddProductAsync(ProductRequestDTO productRequestDTO)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(productRequestDTO);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (productRequestDTO == null)
                //throw new ExceptionMessages("sdasdas");
                throw new ArgumentNullException(nameof(productRequestDTO));

            if (string.IsNullOrEmpty(productRequestDTO.Name))
                throw new ArgumentException("Product name cannot be empty");

            if (productRequestDTO.Price <= 0)
                throw new ArgumentException("Product price must be greater than zero");

            //var product = _mapper.Map<Product>(productRequestDTO);
            var product = new Product
            {
                Price = productRequestDTO.Price,
                Name = productRequestDTO.Name,
                ThumbnailFileName = _myFileUtility.SaveFileInFolder(productRequestDTO.Thumbnail, nameof(Product), false),
                //db => byte[]
                Thumbnail = _myFileUtility.EncryptFile(_myFileUtility.ConvertToByteArray(productRequestDTO.Thumbnail)),
                ThumbnailFileExtenstion = _myFileUtility.GetFileExtension(productRequestDTO.Thumbnail.FileName),
                ThumbnailFileSize = productRequestDTO.Thumbnail.Length
            };

            //product.ThumbnailFileName = _myFileUtility.SaveFileInFolder(productRequestDTO.Thumbnail, nameof(Product), true);
            ////db => byte[]
            //product.Thumbnail = _myFileUtility.EncryptFile(_myFileUtility.ConvertToByteArray(productRequestDTO.Thumbnail));
            //product.ThumbnailFileExtenstion = _myFileUtility.GetFileExtension(productRequestDTO.Thumbnail.FileName);
            //product.ThumbnailFileSize = productRequestDTO.Thumbnail.Length;

            await _productRepository.InsertAsync(product);
        }

        public async Task<bool> RemoveProductAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return false;
            }

            await _productRepository.DeleteAsync(productId);
            return true;
        }



        //In Entity
        public async Task PatchAsync(int id, Dictionary<string, object> updates)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            product.UpdateFromDictionary(updates);
            await _productRepository.UpdateAsync(product);
        }

        public async Task UpdateAsync(ProductRequestDTO productdto)
        {
            var product = await _productRepository.GetByIdAsync(productdto.Id);
            if (product == null) throw new Exception("Product not found");

            product.Update(productdto.Name, productdto.Price, productdto.Description);
            await _productRepository.UpdateAsync(product);
        }

        public async Task UpdateProductPriceAsync(int id, decimal price)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) throw new Exception("Product not found");

            product.UpdatePrice(price);
            await _productRepository.UpdateAsync(product);
        }




        //In Repository
        public async Task UpdateProductAsync(ProductRequestDTO productrequest)
        {
            var product = _mapper.Map<Product>(productrequest);
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task PatchProductAsync(int id, Dictionary<string, object> updates)
        {
            if (updates == null || !updates.Any())
            {
                throw new ArgumentException("Updates cannot be null or empty.", nameof(updates));
            }
            try
            {
                await _productRepository.PatchProductAsync(id, updates);
            }
            catch (ArgumentNullException ex)
            {
                throw new KeyNotFoundException($"Product with ID {id} was not found.", ex);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException($"Failed to update product: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while patching the product.", ex);
            }
        }
    }
}
