using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllElectronicBrandsQueryHandler : IRequestHandler<GetAllElectronicBrandsQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly IElectronicBrandRepository _electronicBrandRepository;

        public GetAllElectronicBrandsQueryHandler(IElectronicBrandRepository electronicBrandRepository) => _electronicBrandRepository = electronicBrandRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllElectronicBrandsQuery request, CancellationToken cancellationToken)
        {
            var result = (await _electronicBrandRepository.GetByElectronicTypeAsync(request.ElectronicTypeId)).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
