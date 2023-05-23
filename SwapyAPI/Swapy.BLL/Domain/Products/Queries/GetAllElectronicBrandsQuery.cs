using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllElectronicBrandsQuery : IRequest<IEnumerable<ElectronicBrand>>
    {
        public string ElectronicTypeId { get; set; }
    }
}
