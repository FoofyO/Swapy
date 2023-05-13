using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class RemoveClothesAttributeCommandHandler : IRequestHandler<RemoveClothesAttributeCommand, Unit>
    {
        private readonly string _userId;
        private readonly IProductRepository _productRepository;
        private readonly IClothesAttributeRepository _clothesAttributeRepository;

        public RemoveClothesAttributeCommandHandler(string userId, IProductRepository productRepository, IClothesAttributeRepository clothesAttributeRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _clothesAttributeRepository = clothesAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveClothesAttributeCommand request, CancellationToken cancellationToken)
        {
            var clothesAttribute = await _clothesAttributeRepository.GetByIdAsync(request.ClothesAttributeId);
            var product = await _productRepository.GetByIdAsync(clothesAttribute.ProductId);

            if (_userId != product.UserId) throw new NoAccessException("No access to delete this product.");

            await _clothesAttributeRepository.DeleteAsync(clothesAttribute);

            return Unit.Value;
        }
    }
}
