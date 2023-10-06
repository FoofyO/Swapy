using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, Unit>
    {
        private readonly IImageService _imageService;
        private readonly IChatRepository _chatRepository;
        private readonly IProductRepository _productRepository;

        public RemoveProductCommandHandler(IImageService imageService, IChatRepository chatRepository, IProductRepository productRepository)
        {
            _imageService = imageService;
            _chatRepository = chatRepository;
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if (!request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to delete this product");

            await _imageService.RemoveAllProductImagesAsync(product.Id);

            await _chatRepository.DeleteChatsByProductId(product.Id);

            await _productRepository.DeleteAsync(product);

            return Unit.Value;
        }
    }
}
