using MediatR;
using Swapy.BLL.Domain.TVs.Queries;
using Swapy.Common.DTO.TVs.Responses;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.TVs.QueryHandlers
{
    public class GetByIdTVAttributeQueryHandler : IRequestHandler<GetByIdTVAttributeQuery, TVAttributeResponseDTO>
    {
        private readonly ITVAttributeRepository _tvAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetByIdTVAttributeQueryHandler(ITVAttributeRepository tvAttributeRepository, ISubcategoryRepository subcategoryRepository)
        {
            _tvAttributeRepository = tvAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<TVAttributeResponseDTO> Handle(GetByIdTVAttributeQuery request, CancellationToken cancellationToken)
        {
            var tvAttribute = await _tvAttributeRepository.GetDetailByIdAsync(request.ProductId);
            List<CategoryNode> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(tvAttribute.Product.SubcategoryId, request.Language)).ToList();
            categories.Insert(0, new CategoryNode(tvAttribute.Product.CategoryId, tvAttribute.Product.Category.Names.FirstOrDefault(l => l.Language == request.Language).Value));

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
                DateTime = tvAttribute.Product.DateTime,
                IsDisable = tvAttribute.Product.IsDisable,
                Categories = categories,
                Images = tvAttribute.Product.Images.Select(i => i.Image).ToList(),
                IsNew = tvAttribute.IsNew,
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
