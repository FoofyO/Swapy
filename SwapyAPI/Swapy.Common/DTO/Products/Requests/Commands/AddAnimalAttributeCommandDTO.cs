using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class AddAnimalAttributeCommandDTO : AddProductCommandDTO<AnimalAttribute>
    {
        public string animalBreedId { get; set; }
    }
}
