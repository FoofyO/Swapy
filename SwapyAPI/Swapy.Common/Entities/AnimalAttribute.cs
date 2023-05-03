namespace Swapy.Common.Entities
{
    public class AnimalAttribute
    {
        public Guid Id { get; set; } 
        public Guid AnimalBreedId { get; set; } 
        public AnimalBreed AnimalBreed { get; set; } 
        public Guid ProductId { get; set; } 
        public Product Product { get; set; }
         
        public AnimalAttribute() { }

        public AnimalAttribute(Guid animalBreedId, Guid productId)
        { 
            ProductId = productId;
            AnimalBreedId = animalBreedId;
        }
    } 
}
