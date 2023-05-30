using MediatR;
using Swapy.BLL.Domain.Categories.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Categories.QueryHandlers
{
    public class GetAllSubcategoriesByCategoryQueryHandler : IRequestHandler<GetAllSubcategoriesByCategoryQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllSubcategoriesByCategoryQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllSubcategoriesByCategoryQuery request, CancellationToken cancellationToken)
        {
            return (await _subcategoryRepository.GetByCategoryAsync(request.CategoryId)).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
        }
    }
}
