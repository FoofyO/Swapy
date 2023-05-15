using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class RemoveAutoAttributeCommandHandler : IRequestHandler<RemoveAutoAttributeCommand, Unit>
    {
        private readonly string _userId;
        private readonly IProductRepository _productRepository;
        private readonly IAutoAttributeRepository _autoAttributeRepository;

        public RemoveAutoAttributeCommandHandler(string userId, IProductRepository productRepository, IAutoAttributeRepository autoAttributeRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _autoAttributeRepository = autoAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveAutoAttributeCommand request, CancellationToken cancellationToken)
        {
            var autoAttribute = await _autoAttributeRepository.GetByIdAsync(request.AutoAttributeId);
            var product = await _productRepository.GetByIdAsync(autoAttribute.ProductId);

            if (!_userId.Equals(product.UserId)) throw new NoAccessException("No access to delete this product.");

            await _autoAttributeRepository.DeleteAsync(autoAttribute);

            return Unit.Value;
        }
    }
}
