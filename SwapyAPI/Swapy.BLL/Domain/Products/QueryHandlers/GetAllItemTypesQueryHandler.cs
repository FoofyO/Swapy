using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllItemTypesQueryHandler : IRequestHandler<GetAllItemTypesQuery, IEnumerable<Subcategory>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllItemTypesQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<Subcategory>> Handle(GetAllItemTypesQuery request, CancellationToken cancellationToken)
        {
            var result = await _subcategoryRepository.GetAllItemTypesAsync();
            return result;
        }
    }
}
