namespace Swapy.BLL.CQRS.Commands
{
    public class AddItemAttributeCommand : AddProductCommand
    {
        public bool IsNew { get; set; }
        public Guid ItemTypeId { get; set; }
    }
}
