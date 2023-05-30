using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.BLL.Services;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllElectronicAttributesQueryHandler : IRequestHandler<GetAllElectronicAttributesQuery, ProductsResponseDTO<ProductResponseDTO>>
    {
        private readonly IElectronicAttributeRepository _electronicAttributeRepository;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public GetAllElectronicAttributesQueryHandler(IElectronicAttributeRepository electronicAttributeRepository, IFavoriteProductRepository favoriteProductRepository)
        {
            _electronicAttributeRepository = electronicAttributeRepository;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<ProductsResponseDTO<ProductResponseDTO>> Handle(GetAllElectronicAttributesQuery request, CancellationToken cancellationToken)
        {
            var query = await _electronicAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Product.Title.Contains(request.Title)) &&
                (request.CurrencyId == null || x.Product.CurrencyId.Equals(request.CurrencyId)) &&
                (request.PriceMin == null) || (x.Product.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Product.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.Product.CategoryId.Equals(request.CategoryId)) &&
                (request.SubcategoryId == null || x.Product.SubcategoryId.Equals(request.SubcategoryId)) &&
                (request.CityId == null || x.Product.CityId.Equals(request.CityId)) &&
                (request.OtherUserId == null ? !x.Product.UserId.Equals(request.UserId) : x.Product.UserId.Equals(request.OtherUserId)) &&
                (request.IsNew == null || x.IsNew == request.IsNew) &&
                (request.MemoriesId == null || request.MemoriesId.Equals(x.MemoryModel.MemoryId)) &&
                (request.ColorsId == null || request.ColorsId.Equals(x.ModelColor.ColorId)) &&
                (request.ModelsId == null || request.ModelsId.Equals(x.MemoryModel.ModelId)) &&
                ((request.BrandsId == null && request.ModelsId != null) || request.BrandsId.Equals(x.MemoryModel.Model.ElectronicBrandType.ElectronicBrandId)) &&
                ((request.TypesId == null && request.ModelsId != null) || request.TypesId.Equals(x.MemoryModel.Model.ElectronicBrandType.ElectronicTypeId)) );
            if (request.SortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (request.ReverseSort == true) query.Reverse();

            FavoriteProductsService favoriteProductsService = new(_favoriteProductRepository);

            var result = await query.Select(x => new ProductResponseDTO()
            {
                Id = x.ProductId,
                Title = x.Product.Title,
                Price = x.Product.Price,
                City = x.Product.City.Name,
                Currency = x.Product.Currency.Name,
                CurrencySymbol = x.Product.Currency.Symbol,
                DateTime = x.Product.DateTime,
                Images = x.Product.Images.Select(i => i.Image).ToList(),
                UserType = x.Product.User.Type
            }).ToListAsync();

            foreach (var item in result)
            {
                item.IsFavorite = await favoriteProductsService.IsFavoriteAsync(item.Id, request.UserId);
            }

            return new ProductsResponseDTO<ProductResponseDTO>(result, query.Count(), (int)Math.Ceiling(Convert.ToDouble(query.Count() / request.PageSize)));
        }
    }
}
