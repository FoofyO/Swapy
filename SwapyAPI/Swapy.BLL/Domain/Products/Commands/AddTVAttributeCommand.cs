namespace Swapy.BLL.Domain.Products.Commands
{
    public class AddTVAttributeCommand : AddProductCommand
    {
        public bool IsNew { get; set; }
        public bool IsSmart { get; set; }
        public Guid TVTypeId { get; set; }
        public Guid TVBrandId { get; set; }
        public Guid ScreenResolutionId { get; set; }
        public Guid ScreenDiagonalId { get; set; }
    }
}
