using MediatR;
using Swapy.BLL.Domain.Categories.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Categories.QueryHandlers
{
    public class GetAllSubcategoriesBySubcategoryQueryHandler : IRequestHandler<GetAllSubcategoriesBySubcategoryQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllSubcategoriesBySubcategoryQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllSubcategoriesBySubcategoryQuery request, CancellationToken cancellationToken)
        {
            return (await _subcategoryRepository.GetBySubcategoryAsync(request.SubcategoryId)).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
        }
    }
}
