using MediatR;
using Swapy.BLL.Domain.Electronics.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Electronics.QueryHandlers
{
    public class GetAllColorsQueryByModelHandler : IRequestHandler<GetAllColorsByModelQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly IColorRepository _colorRepository;

        public GetAllColorsQueryByModelHandler(IColorRepository colorRepository) => _colorRepository = colorRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllColorsByModelQuery request, CancellationToken cancellationToken)
        { 
            var result = (await _colorRepository.GetByModelAsync(request.ModelId)).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
