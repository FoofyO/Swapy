namespace Swapy.BLL.CQRS.Commands
{
    public class AddAnimalAttributeCommand : AddProductCommand
    {
        public Guid AnimalBreedId { get; set; }
    }
}
