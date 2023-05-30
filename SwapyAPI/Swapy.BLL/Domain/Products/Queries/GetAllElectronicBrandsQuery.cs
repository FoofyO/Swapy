using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllElectronicBrandsQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
        public string ElectronicTypeId { get; set; }
    }
}
