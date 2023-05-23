using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllRealEstateTypesQueryHandler : IRequestHandler<GetAllRealEstateTypesQuery, IEnumerable<Subcategory>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllRealEstateTypesQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<Subcategory>> Handle(GetAllRealEstateTypesQuery request, CancellationToken cancellationToken)
        {
            var result = await _subcategoryRepository.GetAllRealEstateTypesAsync();
            return result;
        }
    }
}
