using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllAnimalAttributesQuery : GetAllProductQuery<AnimalAttribute>
    {
        public List<string> AnimalBreedsId { get; set; }
        public List<string> AnimalTypesId { get; set; }
    }
}
