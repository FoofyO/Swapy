using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class UpdateClothesAttributeCommandHandler : IRequestHandler<UpdateClothesAttributeCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IClothesAttributeRepository _clothesAttributeRepository;

        public UpdateClothesAttributeCommandHandler(IProductRepository productRepository, IClothesAttributeRepository clothesAttributeRepository)
        {
            _productRepository = productRepository;
            _clothesAttributeRepository = clothesAttributeRepository;
        }

        public async Task<Unit> Handle(UpdateClothesAttributeCommand request, CancellationToken cancellationToken)
        {

            var clothesAttribute = await _clothesAttributeRepository.GetByProductIdAsync(request.ProductId);
            var product = await _productRepository.GetByIdAsync(clothesAttribute.ProductId);
            
            if (!request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to update this product");

            product.Title = request.Title;
            product.Description = request.Description;
            product.Price = request.Price;
            product.CurrencyId = request.CurrencyId;
            product.CategoryId = request.CategoryId;
            product.SubcategoryId = request.SubcategoryId;
            product.CityId = request.CityId;
            await _productRepository.UpdateAsync(product);

            clothesAttribute.IsNew = request.IsNew;
            clothesAttribute.ClothesSeasonId = request.ClothesSeasonId;
            clothesAttribute.ClothesSizeId = request.ClothesSizeId;
            clothesAttribute.ClothesBrandViewId = request.ClothesBrandViewId;
            await _clothesAttributeRepository.UpdateAsync(clothesAttribute);

            return Unit.Value;
        }
    }
}