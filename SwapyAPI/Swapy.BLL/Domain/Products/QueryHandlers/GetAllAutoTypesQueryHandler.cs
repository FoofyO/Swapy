using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllAutoTypesQueryHandler : IRequestHandler<GetAllAutoTypesQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllAutoTypesQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllAutoTypesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _subcategoryRepository.GetAllAutoTypesAsync()).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
