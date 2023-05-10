using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllAnimalAttributeQuery : GetAllProductQuery<AnimalAttribute>
    {
        public List<string> AnimalBreedsId { get; set; }
        public List<string> AnimalTypesId { get; set; }
    }
}
