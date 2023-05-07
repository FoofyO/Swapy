using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetByIdTVAttributeQuery : IRequest<TVAttribute>
    {
        public Guid TVAttributeId { get; set; }
    }
}
