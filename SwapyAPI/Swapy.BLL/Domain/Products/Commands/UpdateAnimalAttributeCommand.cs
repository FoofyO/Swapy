namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateAnimalAttributeCommand : UpdateProductCommand
    {
        public string AnimalBreedId { get; set; }
    }
}
