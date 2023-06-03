using MediatR;
using Swapy.BLL.Domain.Autos.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Autos.CommandHandlers
{
    public class RemoveAutoAttributeCommandHandler : IRequestHandler<RemoveAutoAttributeCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IAutoAttributeRepository _autoAttributeRepository;

        public RemoveAutoAttributeCommandHandler(IProductRepository productRepository, IAutoAttributeRepository autoAttributeRepository)
        {
            _productRepository = productRepository;
            _autoAttributeRepository = autoAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveAutoAttributeCommand request, CancellationToken cancellationToken)
        {
            var autoAttribute = await _autoAttributeRepository.GetByIdAsync(request.AutoAttributeId);
            var product = await _productRepository.GetByIdAsync(autoAttribute.ProductId);

            if (request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to delete this product");

            await _autoAttributeRepository.DeleteAsync(autoAttribute);

            return Unit.Value;
        }
    }
}
