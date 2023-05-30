using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllScreenResolutionsQueryHandler : IRequestHandler<GetAllScreenResolutionsQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly IScreenResolutionRepository _screenResolutionRepository;

        public GetAllScreenResolutionsQueryHandler(IScreenResolutionRepository screenResolutionRepository) => _screenResolutionRepository = screenResolutionRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllScreenResolutionsQuery request, CancellationToken cancellationToken)
        {
            var result = (await _screenResolutionRepository.GetAllAsync()).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
