using MediatR;
using Swapy.BLL.Domain.TVs.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.DTO.TVs.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.TVs.QueryHandlers
{
    public class GetByIdTVAttributeQueryHandler : IRequestHandler<GetByIdTVAttributeQuery, TVAttributeResponseDTO>
    {
        private readonly ITVAttributeRepository _tvAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public GetByIdTVAttributeQueryHandler(ITVAttributeRepository tvAttributeRepository, ISubcategoryRepository subcategoryRepository, IFavoriteProductRepository favoriteProductRepository)
        {
            _tvAttributeRepository = tvAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<TVAttributeResponseDTO> Handle(GetByIdTVAttributeQuery request, CancellationToken cancellationToken)
        {
            var tvAttribute = await _tvAttributeRepository.GetDetailByIdAsync(request.ProductId);
            List<SpecificationResponseDTO<string>> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(tvAttribute.Product.SubcategoryId, request.Language)).ToList();
            categories.Insert(0, new SpecificationResponseDTO<string>(tvAttribute.Product.CategoryId, tvAttribute.Product.Category.Names.FirstOrDefault(l => l.Language == request.Language).Value));

            TVAttributeResponseDTO result = new TVAttributeResponseDTO()
            {
                Id = tvAttribute.Id,
                City = tvAttribute.Product.City.Names.FirstOrDefault(l => l.Language == request.Language).Value,
                Currency = tvAttribute.Product.Currency.Name,
                CurrencySymbol = tvAttribute.Product.Currency.Symbol,
                UserId = tvAttribute.Product.UserId,
                FirstName = tvAttribute.Product.User.FirstName,
                LastName = tvAttribute.Product.User.LastName,
                PhoneNumber = tvAttribute.Product.User.PhoneNumber,
                ShopId = tvAttribute.Product.User.ShopAttributeId,
                Shop = tvAttribute.Product.User.ShopAttribute.ShopName,
                UserType = tvAttribute.Product.User.Type,
                ProductId = tvAttribute.Product.Id,
                Title = tvAttribute.Product.Title,
                Views = tvAttribute.Product.Views,
                Price = tvAttribute.Product.Price,
                Description = tvAttribute.Product.Description,
                DateTime = tvAttribute.Product.DateTime,
                IsDisable = tvAttribute.Product.IsDisable,
                Categories = categories,
                Images = tvAttribute.Product.Images.Select(i => i.Image).ToList(),
                IsNew = tvAttribute.IsNew,
                IsFavorite = await _favoriteProductRepository.CheckProductOnFavorite(tvAttribute.Product.Id, request.UserId),
                ISmart = tvAttribute.IsSmart,
                TVBrandId = tvAttribute.TVBrandId,
                TVBrand = tvAttribute.TVBrand.Name,
                TVTypeId = tvAttribute.TVTypeId,
                TVType = tvAttribute.TVType.Names.FirstOrDefault(l => l.Language == request.Language).Value,
                ScreenDiagonalId = tvAttribute.ScreenDiagonalId,
                ScreenDiagonal = tvAttribute.ScreenDiagonal.Diagonal,
                ScreenResolutionId = tvAttribute.ScreenResolutionId,
                ScreenResolution = tvAttribute.ScreenResolution.Name
            };
            return result;
        }
    }
}
