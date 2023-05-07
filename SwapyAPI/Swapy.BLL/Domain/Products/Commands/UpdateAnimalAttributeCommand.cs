namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateAnimalAttributeCommand : UpdateProductCommand
    {
        public Guid AnimalAttributeId { get; set; }
        public Guid AnimalBreedId { get; set; }
    }
}
