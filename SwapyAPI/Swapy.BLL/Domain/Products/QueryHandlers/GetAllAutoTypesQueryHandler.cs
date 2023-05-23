using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllAutoTypesQueryHandler : IRequestHandler<GetAllAutoTypesQuery, IEnumerable<Subcategory>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllAutoTypesQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<Subcategory>> Handle(GetAllAutoTypesQuery request, CancellationToken cancellationToken)
        {
            var result = await _subcategoryRepository.GetAllAutoTypesAsync();
            return result;
        }
    }
}
