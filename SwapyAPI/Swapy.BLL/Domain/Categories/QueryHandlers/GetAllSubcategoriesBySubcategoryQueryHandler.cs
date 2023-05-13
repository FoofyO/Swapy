using MediatR;
using Swapy.BLL.Domain.Categories.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Categories.QueryHandlers
{
    public class GetAllSubcategoriesBySubcategoryQueryHandler : IRequestHandler<GetAllSubcategoriesBySubcategoryQuery, IEnumerable<Subcategory>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllSubcategoriesBySubcategoryQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<Subcategory>> Handle(GetAllSubcategoriesBySubcategoryQuery request, CancellationToken cancellationToken)
        {
            return await _subcategoryRepository.GetBySubcategoryAsync(request.SubcategoryId);
        }
    }
}
