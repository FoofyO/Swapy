using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllElectronicTypesQueryHandler : IRequestHandler<GetAllElectronicTypesQuery, IEnumerable<Subcategory>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllElectronicTypesQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<Subcategory>> Handle(GetAllElectronicTypesQuery request, CancellationToken cancellationToken)
        {
            var result = await _subcategoryRepository.GetAllElectronicTypesAsync();
            return result;
        }
    }
}
