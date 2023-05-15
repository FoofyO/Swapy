using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllAnimalBreedsQueryHandler : IRequestHandler<GetAllAnimalBreedsQuery, IEnumerable<AnimalBreed>>
    {
        private readonly IAnimalBreedRepository _animalBreedRepository;

        public GetAllAnimalBreedsQueryHandler(IAnimalBreedRepository animalBreedRepository) => _animalBreedRepository = animalBreedRepository;

        public async Task<IEnumerable<AnimalBreed>> Handle(GetAllAnimalBreedsQuery request, CancellationToken cancellationToken)
        {
            var query = (await _animalBreedRepository.GetQueryableAsync()).OrderBy(x => x.Name);
            var result = await query.ToListAsync();
            return result;
        }
    }
}
