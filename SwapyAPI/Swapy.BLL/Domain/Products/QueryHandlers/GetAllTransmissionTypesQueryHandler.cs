using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllTransmissionTypesQueryHandler : IRequestHandler<GetAllTransmissionTypesQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly ITransmissionTypeRepository _transmissionTypeRepository;

        public GetAllTransmissionTypesQueryHandler(ITransmissionTypeRepository transmissionTypeRepository) => _transmissionTypeRepository = transmissionTypeRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllTransmissionTypesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _transmissionTypeRepository.GetAllAsync()).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
