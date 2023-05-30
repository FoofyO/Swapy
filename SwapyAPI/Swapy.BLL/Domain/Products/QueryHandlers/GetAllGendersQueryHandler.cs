using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllGendersQueryHandler : IRequestHandler<GetAllGendersQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly IGenderRepository _genderRepository;

        public GetAllGendersQueryHandler(IGenderRepository genderRepository) => _genderRepository = genderRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllGendersQuery request, CancellationToken cancellationToken)
        {
            var result = (await _genderRepository.GetAllAsync()).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
