using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllClothesSeasonsQueryHandler : IRequestHandler<GetAllClothesSeasonsQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly IClothesSeasonRepository _clothesSeasonRepository;

        public GetAllClothesSeasonsQueryHandler(IClothesSeasonRepository clothesSeasonRepository) => _clothesSeasonRepository = clothesSeasonRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllClothesSeasonsQuery request, CancellationToken cancellationToken)
        {
            var result = (await _clothesSeasonRepository.GetAllAsync()).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
