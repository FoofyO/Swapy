namespace Swapy.DAL.Entities
{
    public class AnimalType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<AnimalBreed> AnimalBreeds { get; set; }
        
        public AnimalType() => AnimalBreeds = new List<AnimalBreed>();
           
        public AnimalType(string name) : this() 
        {
            Name = name; 
        }
    }
}
   