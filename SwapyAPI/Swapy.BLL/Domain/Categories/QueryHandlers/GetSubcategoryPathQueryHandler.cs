using MediatR;
using Swapy.BLL.Domain.Categories.Queries;
using Swapy.Common.DTO.Categories.Responses;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.Categories.QueryHandlers
{
    public class GetSubcategoryPathQueryHandler : IRequestHandler<GetSubcategoryPathQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly ICategoryRepository _categoryRepository;

        public GetSubcategoryPathQueryHandler(ISubcategoryRepository subcategoryRepository, ICategoryRepository categoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetSubcategoryPathQuery request, CancellationToken cancellationToken)
        {
            List<SpecificationResponseDTO<string>> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(request.SubcategoryId, request.Language)).ToList();
            categories.Insert(0, await _categoryRepository.GetBySubcategoryIdAsync(categories[0]?.Id, request.Language));
            return categories;
        }
    }
}
