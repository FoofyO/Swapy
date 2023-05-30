namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class UpdateTVAttributeCommandDTO : UpdateProductCommandDTO
    {
        public bool isNew { get; set; }
        public bool isSmart { get; set; }
        public string tvTypeId { get; set; }
        public string tvBrandId { get; set; }
        public string screenResolutionId { get; set; }
        public string screenDiagonalId { get; set; }
    }
}
