using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllAnimalTypesQueryHandler : IRequestHandler<GetAllAnimalTypesQuery, IEnumerable<Subcategory>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllAnimalTypesQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<Subcategory>> Handle(GetAllAnimalTypesQuery request, CancellationToken cancellationToken)
        {
            var result = await _subcategoryRepository.GetAllAnimalTypesAsync();
            return result;
        }
    }
}
