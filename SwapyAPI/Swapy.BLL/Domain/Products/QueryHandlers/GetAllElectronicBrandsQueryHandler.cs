using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllElectronicBrandsQueryHandler : IRequestHandler<GetAllElectronicBrandsQuery, IEnumerable<ElectronicBrand>>
    {
        private readonly IElectronicBrandRepository _electronicBrandRepository;

        public GetAllElectronicBrandsQueryHandler(IElectronicBrandRepository electronicBrandRepository) => _electronicBrandRepository = electronicBrandRepository;

        public async Task<IEnumerable<ElectronicBrand>> Handle(GetAllElectronicBrandsQuery request, CancellationToken cancellationToken)
        {
            var result = await _electronicBrandRepository.GetByElectronicTypeAsync(request.ElectronicTypeId);
            return result;
        }
    }
}
