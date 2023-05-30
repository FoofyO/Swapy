using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly ICityRepository _cityRepository;

        public GetAllCitiesQueryHandler(ICityRepository cityRepository) => _cityRepository = cityRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _cityRepository.GetAllAsync()).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
