using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{ 
    public class GetAllItemAttributesQueryHandler : IRequestHandler<GetAllItemAttributesQuery, ProductResponseDTO<ItemAttribute>>
    {
        private readonly string _userId;
        private readonly IItemAttributeRepository _itemAttributeRepository;

        public GetAllItemAttributesQueryHandler(string userId, IItemAttributeRepository itemAttributeRepository)
        {
            _userId = userId;
            _itemAttributeRepository = itemAttributeRepository;
        }

        public async Task<ProductResponseDTO<ItemAttribute>> Handle(GetAllItemAttributesQuery request, CancellationToken cancellationToken)
        {
            var query = await _itemAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Product.Title.Contains(request.Title)) &&
                (request.PriceMin == null) || (x.Product.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Product.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.Product.CategoryId == request.CategoryId) &&
                (request.SubcategoryId == null || x.Product.SubcategoryId == request.SubcategoryId) &&
                (request.CityId == null || x.Product.CityId == request.CityId) &&
                (request.UserId == null ? x.Product.UserId != _userId : x.Product.UserId == request.UserId) &&
                (request.IsNew == null || x.IsNew == request.IsNew) &&
                (request.ItemTypesId == null || request.ItemTypesId.Contains(x.ItemTypeId)) );
            if (request.SortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (request.ReverseSort == true) query.Reverse();
            var result = await query.ToListAsync();
            return new ProductResponseDTO<ItemAttribute>(result, query.Count(), (int)Math.Ceiling(Convert.ToDouble(query.Count() / request.PageSize)));
        }
    }
}
