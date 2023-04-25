namespace Swapy.DAL.Entities
{
    public class AnimalAttribute
    {
        public Guid Id { get; set; } 
        public AnimalBreed AnimalBreed { get; set; } 
        public Guid ProductId { get; set; } 
        public Product Product { get; set; }
         
        public AnimalAttribute() { }

        public AnimalAttribute(Guid AnimalBreedId, Guid productId)
        { 
            AnimalBreedId = AnimalBreedId;
            ProductId = productId;
        }
    } 
}
