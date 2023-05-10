namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateAnimalAttributeCommand : UpdateProductCommand
    {
        public string AnimalAttributeId { get; set; }
        public string AnimalBreedId { get; set; }
    }
}
