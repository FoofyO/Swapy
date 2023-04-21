namespace Swapy.DAL.Entities
{
    public class Subcategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; }

        public Subcategory() => Subcategories = new List<Subcategory>();

        public Subcategory(string name, Guid categoryId)
        {
            Name = name;
            CategoryId = categoryId;
        }
    }
}