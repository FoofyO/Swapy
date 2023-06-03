using MediatR;
using Swapy.BLL.Domain.RealEstates.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.RealEstates.CommandHandlers
{
    public class RemoveRealEstateAttributeCommandHandler : IRequestHandler<RemoveRealEstateAttributeCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IRealEstateAttributeRepository _realEstateAttributeRepository;

        public RemoveRealEstateAttributeCommandHandler(IProductRepository productRepository, IRealEstateAttributeRepository realEstateAttributeRepository)
        {
            _productRepository = productRepository;
            _realEstateAttributeRepository = realEstateAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveRealEstateAttributeCommand request, CancellationToken cancellationToken)
        {
            var realEstateAttribute = await _realEstateAttributeRepository.GetByIdAsync(request.RealEstateAttributeId);
            var product = await _productRepository.GetByIdAsync(realEstateAttribute.ProductId);

            if (request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to delete this product");

            await _realEstateAttributeRepository.DeleteAsync(realEstateAttribute);

            return Unit.Value;
        }
    }
}
