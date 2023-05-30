using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Queries
{
    public class GetAllAnimalAttributesQueryDTO : GetAllProductQueryDTO<AnimalAttribute>
    {
        public List<string> animalBreedsId { get; set; }
        public List<string> animalTypesId { get; set; }
    }
}
