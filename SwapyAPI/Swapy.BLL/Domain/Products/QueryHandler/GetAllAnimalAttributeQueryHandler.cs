using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.BLL.Domain.Products.QueryHandler
{
    public class GetAllAnimalAttributeQueryHandler : IRequestHandler<GetAllAnimalAttributeQuery, IEnumerable<AnimalAttribute>>
    {
        private readonly Guid _userId;
        private readonly IAnimalAttributeRepository _animalAttributeRepository;

        public GetAllAnimalAttributeQueryHandler(Guid userId, IAnimalAttributeRepository animalAttributeRepository)
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
