using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllAnimalTypesQueryHandler : IRequestHandler<GetAllAnimalTypesQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllAnimalTypesQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllAnimalTypesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _subcategoryRepository.GetAllAnimalTypesAsync()).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
