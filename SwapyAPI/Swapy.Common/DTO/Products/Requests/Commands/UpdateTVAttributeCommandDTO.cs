namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class UpdateTVAttributeCommandDTO : UpdateProductCommandDTO
    {
        public string TVAttributeId { get; set; }
        public bool IsNew { get; set; }
        public bool IsSmart { get; set; }
        public string TVTypeId { get; set; }
        public string TVBrandId { get; set; }
        public string ScreenResolutionId { get; set; }
        public string ScreenDiagonalId { get; set; }
    }
}
