namespace Swapy.BLL.CQRS.Commands
{
    public class AddElectronicAttributeCommand : AddProductCommand
    {
        public bool IsNew { get; set; }
        public Guid MemoryModelId { get; set; }
        public Guid ModelColorId { get; set; }
    }
}
