using MediatR;
using Swapy.BLL.Domain.Autos.Queries;
using Swapy.Common.DTO.Autos.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Autos.QueryHandlers
{
    public class GetByIdAutoAttributeQueryHandler : IRequestHandler<GetByIdAutoAttributeQuery, AutoAttributeResponseDTO>
    {
        private readonly IAutoAttributeRepository _autoAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetByIdAutoAttributeQueryHandler(IAutoAttributeRepository autoAttributeRepository, ISubcategoryRepository subcategoryRepository)
        {
            _autoAttributeRepository = autoAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<AutoAttributeResponseDTO> Handle(GetByIdAutoAttributeQuery request, CancellationToken cancellationToken)
        {
            var autoAttribute = await _autoAttributeRepository.GetDetailByIdAsync(request.ProductId);
            List<CategoryNode> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(autoAttribute.Product.SubcategoryId)).Select(s => new CategoryNode(s.Id, s.Name)).ToList();
            categories.Insert(0, new CategoryNode(autoAttribute.Product.CategoryId, autoAttribute.Product.Category.Name));

            AutoAttributeResponseDTO result = new AutoAttributeResponseDTO()
            {
                Id = autoAttribute.Id,
                City = autoAttribute.Product.City.Name,
                Currency = autoAttribute.Product.Currency.Name,
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
                DateTime = autoAttribute.Product.DateTime,
                Categories = categories,
                Images = autoAttribute.Product.Images.Select(i => i.Image).ToList(),
                IsNew = autoAttribute.IsNew,
                IsDisable = autoAttribute.Product.IsDisable,
                Miliage = autoAttribute.Miliage,
                EngineCapacity = autoAttribute.EngineCapacity,
                ReleaseYear = autoAttribute.ReleaseYear,
                FuelTypeId = autoAttribute.FuelTypeId,
                FuelType = autoAttribute.FuelType.Name,
                ColorId = autoAttribute.AutoColorId,
                Color = autoAttribute.AutoColor.Name,
                TransmissionTypeId = autoAttribute.TransmissionTypeId,
                TransmissionType = autoAttribute.TransmissionType.Name,
                AutoBrandId = autoAttribute.AutoModel.AutoBrandId,
                AutoBrand = autoAttribute.AutoModel.AutoBrand.Name
            };
            return result;
        }
    }
}
