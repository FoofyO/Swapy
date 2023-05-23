namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class UpdateAnimalAttributeCommandDTO : UpdateProductCommandDTO
    {
        public string AnimalAttributeId { get; set; }
        public string AnimalBreedId { get; set; }
    }
}
