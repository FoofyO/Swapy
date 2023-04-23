namespace Swapy.DAL.Entities
{
    public class Model
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ElectronicBrandTypeId { get; set; }
        public ElectronicBrandType ElectronicBrandType { get; set; }
        public ICollection<MemoryModel> MemoriesModels { get; set; }
        public ICollection<ModelColor> ModelsColors { get; set; }

        public Model()
        {
            MemoriesModels = new List<MemoryModel>();
            ModelsColors = new List<ModelColor>();
        }

        public Model(string name, Guid electronicBrandTypeId) : this() 
        {
            Name = name;
            ElectronicBrandTypeId = electronicBrandTypeId;
        }
    }
}
