using MediatR;
using Swapy.BLL.CQRS.Commands;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class RemoveAutoAttributeCommandHandler : IRequestHandler<RemoveAutoAttributeCommand, Unit>
    {
        private readonly Guid _userId;
        private readonly IProductRepository _productRepository;
        private readonly IAutoAttributeRepository _autoAttributeRepository;

        public RemoveAutoAttributeCommandHandler(Guid userId, IProductRepository productRepository, IAutoAttributeRepository autoAttributeRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _autoAttributeRepository = autoAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveAutoAttributeCommand request, CancellationToken cancellationToken)
        {
            AutoAttribute autoAttribute = await _autoAttributeRepository.GetByIdAsync(request.AutoAttributeId);
            Product product = await _productRepository.GetByIdAsync(autoAttribute.ProductId);

            if (_userId != product.UserId) throw new NoAccessException("No access to uninstall this product.");

            await _autoAttributeRepository.DeleteAsync(autoAttribute);

            return Unit.Value;
        }
    }
}
