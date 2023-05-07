namespace Swapy.BLL.Domain.Products.Commands
{
    public class AddAnimalAttributeCommand : AddProductCommand
    {
        public Guid AnimalBreedId { get; set; }
    }
}
