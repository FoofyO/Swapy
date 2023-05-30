using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllClothesSizesQueryHandler : IRequestHandler<GetAllClothesSizesQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly IClothesSizeRepository _clothesSizeRepository;

        public GetAllClothesSizesQueryHandler(IClothesSizeRepository clothesSizeRepository) => _clothesSizeRepository = clothesSizeRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllClothesSizesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _clothesSizeRepository.GetByChildAndShoeAsync(request.IsChild, request.IsShoe)).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
