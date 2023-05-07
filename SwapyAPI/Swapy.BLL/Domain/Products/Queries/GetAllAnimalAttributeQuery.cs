using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllAnimalAttributeQuery : GetAllProductQuery<AnimalAttribute>
    {
        public List<Guid> AnimalBreedsId { get; set; }
        public List<Guid> AnimalTypesId { get; set; }
    }
}
