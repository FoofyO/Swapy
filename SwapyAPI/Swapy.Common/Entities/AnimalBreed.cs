namespace Swapy.Common.Entities
{
    public class AnimalBreed
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AnimalTypeId { get; set; } 
        public Subcategory AnimalType { get; set; } 
        public ICollection<AnimalAttribute> AnimalAttributes { get; set; }

        public AnimalBreed() => AnimalAttributes = new List<AnimalAttribute>();
         
        public AnimalBreed(string name, Guid animalTypeId) : this() 
        {
            Name = name;
            AnimalTypeId = animalTypeId;
        } 
    } 
}  