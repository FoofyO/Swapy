using MediatR;
using Swapy.BLL.Domain.Items.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Items.QueryHandlers
{
    public class GetAllItemTypesQueryHandler : IRequestHandler<GetAllItemTypesQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllItemTypesQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllItemTypesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _subcategoryRepository.GetAllItemTypesAsync()).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
