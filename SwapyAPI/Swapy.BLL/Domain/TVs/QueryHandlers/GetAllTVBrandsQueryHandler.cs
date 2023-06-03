using MediatR;
using Swapy.BLL.Domain.TVs.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.TVs.QueryHandlers
{
    public class GetAllTVBrandsQueryHandler : IRequestHandler<GetAllTVBrandsQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly ITVBrandRepository _tvBrandRepository;

        public GetAllTVBrandsQueryHandler(ITVBrandRepository tvBrandRepository) => _tvBrandRepository = tvBrandRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllTVBrandsQuery request, CancellationToken cancellationToken)
        {
            var result = (await _tvBrandRepository.GetAllAsync()).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
