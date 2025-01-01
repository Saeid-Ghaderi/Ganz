using Ganz.Domain.Contracts;
using Ganz.Domain.Enttiies;
using MediatR;

namespace Ganz.Application.CQRS.ProductCommandQuery.Command
{
    public class SaveProductCommand : IRequest<SaveProductCommandResponse>
    {
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public long Price { get; set; }
        public string Description { get; set; }
    }

    public class SaveProductCommandResponse
    {
        public int ProductId { get; set; }
    }

    public class SaveProductCommandHandler : IRequestHandler<SaveProductCommand, SaveProductCommandResponse>
    {
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;

        //private readonly OnlineShopDbContext onlineShopDbContext;

        //public SaveProductCommandHandler(OnlineShopDbContext onlineShopDbContext)
        //{
        //    this.onlineShopDbContext = onlineShopDbContext;
        //}

        public SaveProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<SaveProductCommandResponse> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.ProductName,
                Price = request.Price,
            };

            //await onlineShopDbContext.Products.AddAsync(product);
            //await onlineShopDbContext.SaveChangesAsync();

            await productRepository.InsertAsync(product);
            await unitOfWork.SaveChangesAsync();

            var response = new SaveProductCommandResponse
            {
                ProductId = product.Id
            };

            return response;
        }
    }
}
