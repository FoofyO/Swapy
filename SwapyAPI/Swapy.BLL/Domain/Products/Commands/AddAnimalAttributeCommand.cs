namespace Swapy.BLL.Domain.Products.Commands
{
    public class AddAnimalAttributeCommand : AddProductCommand
    {
        public string AnimalBreedId { get; set; }
    }
}
