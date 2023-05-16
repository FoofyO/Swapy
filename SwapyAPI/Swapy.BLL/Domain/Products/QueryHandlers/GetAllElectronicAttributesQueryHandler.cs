using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllElectronicAttributesQueryHandler : IRequestHandler<GetAllElectronicAttributesQuery, ProductResponseDTO<ElectronicAttribute>>
    {
        private readonly string _userId;
        private readonly IElectronicAttributeRepository _electronicAttributeRepository;

        public GetAllElectronicAttributesQueryHandler(string userId, IElectronicAttributeRepository electronicAttributeRepository)
        {
            _userId = userId;
            _electronicAttributeRepository = electronicAttributeRepository;
        }

        public async Task<ProductResponseDTO<ElectronicAttribute>> Handle(GetAllElectronicAttributesQuery request, CancellationToken cancellationToken)
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
                (request.UserId == null ? !x.Product.UserId.Equals(_userId) : x.Product.UserId.Equals(request.UserId)) &&
                (request.IsNew == null || x.IsNew == request.IsNew) &&
                (request.MemoriesId == null || request.MemoriesId.Equals(x.MemoryModel.MemoryId)) &&
                (request.ColorsId == null || request.ColorsId.Equals(x.ModelColor.ColorId)) &&
                (request.ModelsId == null || request.ModelsId.Equals(x.MemoryModel.ModelId)) &&
                ((request.BrandsId == null && request.ModelsId != null) || request.BrandsId.Equals(x.MemoryModel.Model.ElectronicBrandType.ElectronicBrandId)) &&
                ((request.TypesId == null && request.ModelsId != null) || request.TypesId.Equals(x.MemoryModel.Model.ElectronicBrandType.ElectronicTypeId)) );
            if (request.SortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (request.ReverseSort == true) query.Reverse();
            var result = await query.ToListAsync();
            return new ProductResponseDTO<ElectronicAttribute>(result, query.Count(), (int)Math.Ceiling(Convert.ToDouble(query.Count() / request.PageSize)));
        }
    }
}
