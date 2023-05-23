using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Queries
{
    public class GetAllAnimalAttributesQueryDTO : GetAllProductQueryDTO<AnimalAttribute>
    {
        public List<string> AnimalBreedsId { get; set; }
        public List<string> AnimalTypesId { get; set; }
    }
}
