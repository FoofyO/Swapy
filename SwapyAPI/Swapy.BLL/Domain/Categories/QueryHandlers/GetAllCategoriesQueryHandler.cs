using MediatR;
using Swapy.BLL.Domain.Categories.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Categories.QueryHandlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return (await _categoryRepository.GetAllAsync()).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
        }
    }
}
