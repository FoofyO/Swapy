using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetByIdAutoAttributeQuery : IRequest<AutoAttribute>
    {
        public Guid AutoAttributeId { get; set; }
    }
}
