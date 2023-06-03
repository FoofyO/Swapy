using MediatR;
using Swapy.BLL.Domain.Electronics.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Electronics.QueryHandlers
{
    public class GetAllElectronicTypesQueryHandler : IRequestHandler<GetAllElectronicTypesQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllElectronicTypesQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllElectronicTypesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _subcategoryRepository.GetAllElectronicTypesAsync()).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
