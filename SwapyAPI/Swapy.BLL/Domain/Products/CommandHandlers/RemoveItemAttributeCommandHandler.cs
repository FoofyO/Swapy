using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class RemoveItemAttributeCommandHandler : IRequestHandler<RemoveItemAttributeCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IItemAttributeRepository _itemAttributeRepository;

        public RemoveItemAttributeCommandHandler(IProductRepository productRepository, IItemAttributeRepository itemAttributeRepository)
        {
            _productRepository = productRepository;
            _itemAttributeRepository = itemAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveItemAttributeCommand request, CancellationToken cancellationToken)
        {
            var itemAttribute = await _itemAttributeRepository.GetByIdAsync(request.ItemAttributeId);
            var product = await _productRepository.GetByIdAsync(itemAttribute.ProductId);

            if (request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to delete this product");

            await _itemAttributeRepository.DeleteAsync(itemAttribute);

            return Unit.Value;
        }
    }
}
