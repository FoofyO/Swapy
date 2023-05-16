using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class RemoveRealEstateAttributeCommandHandler : IRequestHandler<RemoveRealEstateAttributeCommand, Unit>
    {
        private readonly string _userId;
        private readonly IProductRepository _productRepository;
        private readonly IRealEstateAttributeRepository _realEstateAttributeRepository;

        public RemoveRealEstateAttributeCommandHandler(string userId, IProductRepository productRepository, IRealEstateAttributeRepository realEstateAttributeRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _realEstateAttributeRepository = realEstateAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveRealEstateAttributeCommand request, CancellationToken cancellationToken)
        {
            var realEstateAttribute = await _realEstateAttributeRepository.GetByIdAsync(request.RealEstateAttributeId);
            var product = await _productRepository.GetByIdAsync(realEstateAttribute.ProductId);

            if (_userId.Equals(product.UserId)) throw new NoAccessException("No access to delete this product");

            await _realEstateAttributeRepository.DeleteAsync(realEstateAttribute);

            return Unit.Value;
        }
    }
}
