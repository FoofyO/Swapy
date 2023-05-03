namespace Swapy.Common.Entities
{
    public class Memory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<MemoryModel> MemoriesModels { get; set; }

        public Memory() => MemoriesModels = new List<MemoryModel>();

        public Memory(string name) : this() => Name = name;
    }
}
