using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetByIdElectronicAttributeQuery : IRequest<ElectronicAttribute>
    {
        public string ElectronicAttributeId { get; set; }
    }
}
