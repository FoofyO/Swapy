namespace Swapy.Common.Entities
{
    public class AnimalBreed
    {
        public string Id { get; set; }
        public string AnimalTypeId { get; set; } 
        public Subcategory AnimalType { get; set; } 
        public ICollection<LocalizationValue> Names { get; set; } = new List<LocalizationValue>();
        public ICollection<AnimalAttribute> AnimalAttributes { get; set; } = new List<AnimalAttribute>();

        public AnimalBreed() => Id = Guid.NewGuid().ToString();

        public AnimalBreed(string animalTypeId) : this() => AnimalTypeId = animalTypeId;
    } 
}  