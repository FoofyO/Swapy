using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllAnimalBreedsQueryHandler : IRequestHandler<GetAllAnimalBreedsQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly IAnimalBreedRepository _animalBreedRepository;

        public GetAllAnimalBreedsQueryHandler(IAnimalBreedRepository animalBreedRepository) => _animalBreedRepository = animalBreedRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllAnimalBreedsQuery request, CancellationToken cancellationToken)
        { 
            var result = (await _animalBreedRepository.GetByAnimalTypeAsync(request.AnimalTypesId)).Select(x => new SpecificationResponseDTO(x.Id, x.Name)).ToList();
            return result;
        }
    }
}
