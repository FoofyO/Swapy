using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.BLL.Interfaces;
using Swapy.BLL.Services;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class AddClothesAttributeCommandHandler : IRequestHandler<AddClothesAttributeCommand, ClothesAttribute>
    {
        private readonly string _userId;
        private readonly IProductRepository _productRepository;
        private readonly IClothesAttributeRepository _clothesAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public AddClothesAttributeCommandHandler(string userId, IProductRepository productRepository, IClothesAttributeRepository clothesAttributeRepository, ISubcategoryRepository subcategoryRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _clothesAttributeRepository = clothesAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<ClothesAttribute> Handle(AddClothesAttributeCommand request, CancellationToken cancellationToken)
        {
            ISubcategoryService subcategoryService = new SubcategoryService(_subcategoryRepository);
            if (!await subcategoryService.SubcategoryValidationAsync(request.SubcategoryId)) throw new ArgumentException("Invalid subcategory.");

            Product product = new Product(request.Title, request.Description, request.Price, _userId, request.CurrencyId, request.CategoryId, request.SubcategoryId, request.CityId);
            await _productRepository.CreateAsync(product);

            ClothesAttribute clothesAttribute = new ClothesAttribute(request.IsNew, request.ClothesSeasonId, request.ClothesSizeId, request.ClothesBrandViewId, product.Id);
            await _clothesAttributeRepository.CreateAsync(clothesAttribute);

            return clothesAttribute;
        }
    }
}
