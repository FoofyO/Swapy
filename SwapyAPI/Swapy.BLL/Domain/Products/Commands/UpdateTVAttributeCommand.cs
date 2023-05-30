namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateTVAttributeCommand : UpdateProductCommand
    {
        public bool IsNew { get; set; }
        public bool IsSmart { get; set; }
        public string TVTypeId { get; set; }
        public string TVBrandId { get; set; }
        public string ScreenResolutionId { get; set; }
        public string ScreenDiagonalId { get; set; }
    }
}
