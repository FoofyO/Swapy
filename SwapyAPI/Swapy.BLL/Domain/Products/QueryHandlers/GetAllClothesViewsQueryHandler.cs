using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllClothesViewsQueryHandler : IRequestHandler<GetAllClothesViewsQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly IClothesViewRepository _clothesViewRepository;

        public GetAllClothesViewsQueryHandler(IClothesViewRepository clothesViewRepository) => _clothesViewRepository = clothesViewRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllClothesViewsQuery request, CancellationToken cancellationToken)
        {
            var result = (await _clothesViewRepository.GetByGenderAndTypeAsync(request.GenderId, request.ClothesTypeId)).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
