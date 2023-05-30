namespace Swapy.Common.Models
{
    public class CategoryNode
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public CategoryNode() { }

        public CategoryNode(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
