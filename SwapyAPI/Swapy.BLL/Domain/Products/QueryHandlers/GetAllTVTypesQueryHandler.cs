using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllTVTypesQueryHandler : IRequestHandler<GetAllTVTypesQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly ITVTypeRepository _tvTypeRepository;

        public GetAllTVTypesQueryHandler(ITVTypeRepository tvTypeRepository) => _tvTypeRepository = tvTypeRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllTVTypesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _tvTypeRepository.GetAllAsync()).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
