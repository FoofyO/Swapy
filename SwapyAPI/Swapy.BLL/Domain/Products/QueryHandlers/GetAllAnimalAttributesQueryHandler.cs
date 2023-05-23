using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllAnimalAttributesQueryHandler : IRequestHandler<GetAllAnimalAttributesQuery, ProductResponseDTO<AnimalAttribute>>
    {
        private readonly IAnimalAttributeRepository _animalAttributeRepository;

        public GetAllAnimalAttributesQueryHandler(IAnimalAttributeRepository animalAttributeRepository)
        {
            _animalAttributeRepository = animalAttributeRepository;
        }

        public async Task<ProductResponseDTO<AnimalAttribute>> Handle(GetAllAnimalAttributesQuery request, CancellationToken cancellationToken)
        {
            var query = await _animalAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Product.Title.Contains(request.Title)) &&
                (request.CurrencyId == null || x.Product.CurrencyId.Equals(request.CurrencyId)) &&
                (request.PriceMin == null) || (x.Product.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Product.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.Product.CategoryId.Equals(request.CategoryId)) &&
                (request.SubcategoryId == null || x.Product.SubcategoryId.Equals(request.SubcategoryId)) &&
                (request.CityId == null || x.Product.CityId.Equals(request.CityId)) &&
                (request.OtherUserId == null ? !x.Product.UserId.Equals(request.UserId) : x.Product.UserId.Equals(request.OtherUserId)) &&
                (request.AnimalBreedsId == null || request.AnimalBreedsId.Equals(x.AnimalBreedId)) &&
                (request.AnimalTypesId == null || request.AnimalTypesId.Equals(x.AnimalBreed.AnimalTypeId)) );
            if (request.SortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (request.ReverseSort == true) query.Reverse();
            var result = await query.ToListAsync();
            return new ProductResponseDTO<AnimalAttribute>(result, query.Count(), (int)Math.Ceiling(Convert.ToDouble(query.Count() / request.PageSize)));
        }
    }
}
