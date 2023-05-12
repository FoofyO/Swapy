using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class AddAnimalAttributeCommand : AddProductCommand<AnimalAttribute>
    {
        public string AnimalBreedId { get; set; }
    }
}
