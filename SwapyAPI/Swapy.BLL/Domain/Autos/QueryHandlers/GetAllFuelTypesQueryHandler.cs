using MediatR;
using Swapy.BLL.Domain.Autos.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Autos.QueryHandlers
{
    public class GetAllFuelTypesQueryHandler : IRequestHandler<GetAllFuelTypesQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly IFuelTypeRepository _fuelTypeRepository;

        public GetAllFuelTypesQueryHandler(IFuelTypeRepository fuelTypeRepository) => _fuelTypeRepository = fuelTypeRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllFuelTypesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _fuelTypeRepository.GetAllAsync()).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
