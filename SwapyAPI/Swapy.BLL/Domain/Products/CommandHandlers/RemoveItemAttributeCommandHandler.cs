using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class RemoveItemAttributeCommandHandler : IRequestHandler<RemoveItemAttributeCommand, Unit>
    {
        private readonly Guid _userId;
        private readonly IProductRepository _productRepository;
        private readonly IItemAttributeRepository _itemAttributeRepository;

        public RemoveItemAttributeCommandHandler(Guid userId, IProductRepository productRepository, IItemAttributeRepository itemAttributeRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _itemAttributeRepository = itemAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveItemAttributeCommand request, CancellationToken cancellationToken)
        {
            ItemAttribute itemAttribute = await _itemAttributeRepository.GetByIdAsync(request.ItemAttributeId);
            Product product = await _productRepository.GetByIdAsync(itemAttribute.ProductId);

            if (_userId != product.UserId) throw new NoAccessException("No access to uninstall this product.");

            await _itemAttributeRepository.DeleteAsync(itemAttribute);

            return Unit.Value;
        }
    }
}
