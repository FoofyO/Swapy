using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllColorsQueryHandler : IRequestHandler<GetAllColorsQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly IColorRepository _colorRepository;

        public GetAllColorsQueryHandler(IColorRepository colorRepository) => _colorRepository = colorRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllColorsQuery request, CancellationToken cancellationToken)
        { 
            var result = (await _colorRepository.GetAllAsync()).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
