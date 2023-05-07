using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetByIdAnimalAttributeQuery : IRequest<AnimalAttribute>
    {
        public Guid AnimalAttributeId { get; set; }
    }
}
