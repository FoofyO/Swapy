using MediatR;
using Swapy.BLL.Domain.Autos.Queries;
using Swapy.Common.DTO.Autos.Responses;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.Autos.QueryHandlers
{
    public class GetByIdAutoAttributeQueryHandler : IRequestHandler<GetByIdAutoAttributeQuery, AutoAttributeResponseDTO>
    {
        private readonly IAutoAttributeRepository _autoAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public GetByIdAutoAttributeQueryHandler(IAutoAttributeRepository autoAttributeRepository, ISubcategoryRepository subcategoryRepository, IFavoriteProductRepository favoriteProductRepository)
        {
            _autoAttributeRepository = autoAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<AutoAttributeResponseDTO> Handle(GetByIdAutoAttributeQuery request, CancellationToken cancellationToken)
        {
            var autoAttribute = await _autoAttributeRepository.GetDetailByIdAsync(request.ProductId);
            List<SpecificationResponseDTO<string>> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(autoAttribute.Product.SubcategoryId, request.Language)).ToList();
            categories.Insert(0, new SpecificationResponseDTO<string>(autoAttribute.Product.CategoryId, autoAttribute.Product.Category.Names.FirstOrDefault(l => l.Language == request.Language).Value));

            AutoAttributeResponseDTO result = new AutoAttributeResponseDTO()
            {
                Id = autoAttribute.Id,
                City = autoAttribute.Product.City.Names.FirstOrDefault(l => l.Language == request.Language).Value,
                CityId = autoAttribute.Product.City.Id,
                Currency = autoAttribute.Product.Currency.Name,
                CurrencyId = autoAttribute.Product.Currency.Id,
                CurrencySymbol = autoAttribute.Product.Currency.Symbol,
                UserId = autoAttribute.Product.UserId,
                FirstName = autoAttribute.Product.User.FirstName,
                LastName = autoAttribute.Product.User.LastName,
                PhoneNumber = autoAttribute.Product.User.PhoneNumber,
                ShopId = autoAttribute.Product.User.ShopAttributeId,
                Shop = autoAttribute.Product.User.ShopAttribute.ShopName,
                UserType = autoAttribute.Product.User.Type,
                ProductId = autoAttribute.Product.Id,
                Title = autoAttribute.Product.Title,
                Views = autoAttribute.Product.Views,
                Price = autoAttribute.Product.Price,
                Description = autoAttribute.Product.Description,
                DateTime = autoAttribute.Product.DateTime,
                Categories = categories,
                Images = autoAttribute.Product.Images.Select(i => i.Image).ToList(),
                IsNew = autoAttribute.IsNew,
                IsFavorite = await _favoriteProductRepository.CheckProductOnFavorite(autoAttribute.Product.Id, request.UserId),
                IsDisable = autoAttribute.Product.IsDisable,
                Miliage = autoAttribute.Miliage,
                EngineCapacity = autoAttribute.EngineCapacity,
                ReleaseYear = autoAttribute.ReleaseYear,
                FuelTypeId = autoAttribute.FuelTypeId,
                FuelType = autoAttribute.FuelType.Names.FirstOrDefault(l => l.Language == request.Language).Value,
                ColorId = autoAttribute.AutoColorId,
                Color = autoAttribute.AutoColor.Names.FirstOrDefault(l => l.Language == request.Language).Value,
                TransmissionTypeId = autoAttribute.TransmissionTypeId,
                TransmissionType = autoAttribute.TransmissionType.Names.FirstOrDefault(l => l.Language == request.Language).Value,
                AutoBrandId = autoAttribute.AutoModel.AutoBrandId,
                AutoBrand = autoAttribute.AutoModel.AutoBrand.Name
            };
            return result;
        }
    }
}
