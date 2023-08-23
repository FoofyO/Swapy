using MediatR;
using Swapy.BLL.Domain.Clothes.Queries;
using Swapy.Common.DTO.Clothes.Responses;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;
using System.Runtime.InteropServices;

namespace Swapy.BLL.Domain.Clothes.QueryHandlers
{
    public class GetByIdClothesAttributeQueryHandler : IRequestHandler<GetByIdClothesAttributeQuery, ClothesAttributeResponseDTO>
    {
        private readonly IClothesAttributeRepository _clothesAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public GetByIdClothesAttributeQueryHandler(IClothesAttributeRepository clothesAttributeRepository, ISubcategoryRepository subcategoryRepository, IFavoriteProductRepository favoriteProductRepository)
        {
            _clothesAttributeRepository = clothesAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<ClothesAttributeResponseDTO> Handle(GetByIdClothesAttributeQuery request, CancellationToken cancellationToken)
        {
            var clothesAttribute = await _clothesAttributeRepository.GetDetailByIdAsync(request.ProductId);
            List<SpecificationResponseDTO<string>> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(clothesAttribute.Product.SubcategoryId, request.Language)).ToList();
            categories.Insert(0, new SpecificationResponseDTO<string>(clothesAttribute.Product.CategoryId, clothesAttribute.Product.Category.Names.FirstOrDefault(l => l.Language == request.Language).Value));

            ClothesAttributeResponseDTO result = new ClothesAttributeResponseDTO()
            {
                Id = clothesAttribute.Id,
                City = clothesAttribute.Product.City.Names.FirstOrDefault(l => l.Language == request.Language).Value,
                CityId = clothesAttribute.Product.City.Id,
                Currency = clothesAttribute.Product.Currency.Name,
                CurrencyId = clothesAttribute.Product.Currency.Id,
                CurrencySymbol = clothesAttribute.Product.Currency.Symbol,
                UserId = clothesAttribute.Product.UserId,
                FirstName = clothesAttribute.Product.User.FirstName,
                LastName = clothesAttribute.Product.User.LastName,
                PhoneNumber = clothesAttribute.Product.User.PhoneNumber,
                ShopId = clothesAttribute.Product.User.ShopAttributeId,
                Shop = clothesAttribute.Product.User.ShopAttribute.ShopName,
                UserType = clothesAttribute.Product.User.Type,
                ProductId = clothesAttribute.Product.Id,
                Title = clothesAttribute.Product.Title,
                Views = clothesAttribute.Product.Views,
                Price = clothesAttribute.Product.Price,
                Description = clothesAttribute.Product.Description,
                DateTime = clothesAttribute.Product.DateTime,
                Categories = categories,
                Images = clothesAttribute.Product.Images.Select(i => i.Image).ToList(),
                IsNew = clothesAttribute.IsNew,
                IsFavorite = await _favoriteProductRepository.CheckProductOnFavorite(clothesAttribute.Product.Id, request.UserId),
                ClothesSizeId = clothesAttribute.ClothesSizeId,
                ClothesSize = clothesAttribute.ClothesSize.Name,
                IsShoe = clothesAttribute.ClothesSize.IsShoe,
                IsChild = clothesAttribute.ClothesSize.IsChild,
                GenderId = clothesAttribute.ClothesBrandView.ClothesView.GenderId,
                Gender = clothesAttribute.ClothesBrandView.ClothesView.Gender.Names.FirstOrDefault(l => l.Language == request.Language).Value,
                ClothesSeasonId = clothesAttribute.ClothesSeasonId,
                ClothesSeason = clothesAttribute.ClothesSeason.Names.FirstOrDefault(l => l.Language == request.Language).Value,
                ClothesBrandId = clothesAttribute.ClothesBrandView.ClothesBrandId,
                ClothesBrand = clothesAttribute.ClothesBrandView.ClothesBrand.Name,
                ClothesViewId = clothesAttribute.ClothesBrandView.ClothesViewId,
                ClothesView = clothesAttribute.ClothesBrandView.ClothesView.Names.FirstOrDefault(l => l.Language == request.Language).Value
            };
            return result;
        }
    }
}
