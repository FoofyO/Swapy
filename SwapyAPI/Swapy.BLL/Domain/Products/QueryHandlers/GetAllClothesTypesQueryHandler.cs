using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllClothesTypesQueryHandler : IRequestHandler<GetAllClothesTypesQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllClothesTypesQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllClothesTypesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _subcategoryRepository.GetClothesTypesByGenderAsync(request.GenderId)).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
