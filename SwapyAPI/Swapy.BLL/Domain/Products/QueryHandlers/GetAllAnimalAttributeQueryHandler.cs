using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllAnimalAttributeQueryHandler : IRequestHandler<GetAllAnimalAttributeQuery, IEnumerable<AnimalAttribute>>
    {
        private readonly string _userId;
        private readonly IAnimalAttributeRepository _animalAttributeRepository;

        public GetAllAnimalAttributeQueryHandler(string userId, IAnimalAttributeRepository animalAttributeRepository)
        {
            _userId = userId;
            _animalAttributeRepository = animalAttributeRepository;
        }

        public async Task<IEnumerable<AnimalAttribute>> Handle(GetAllAnimalAttributeQuery request, CancellationToken cancellationToken)
        {
            var query = await _animalAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Product.Title.Contains(request.Title)) &&
                (request.PriceMin == null) || (x.Product.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Product.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.Product.CategoryId == request.CategoryId) &&
                (request.SubcategoryId == null || x.Product.SubcategoryId == request.SubcategoryId) &&
                (request.CityId == null || x.Product.CityId == request.CityId) &&
                (request.UserId == null ? x.Product.UserId != _userId : x.Product.UserId == request.UserId) &&
                (request.AnimalBreedsId == null || request.AnimalBreedsId.Contains(x.AnimalBreedId)) &&
                (request.AnimalTypesId == null || request.AnimalTypesId.Contains(x.AnimalBreed.AnimalTypeId)) );
            if (request.SortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (request.ReverseSort == true) query.Reverse();
            var result = await query.ToListAsync();

            return result;
        }
    }
}
