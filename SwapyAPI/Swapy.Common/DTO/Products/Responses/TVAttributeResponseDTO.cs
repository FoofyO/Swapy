namespace Swapy.Common.DTO.Products.Responses
{
    public class TVAttributeResponseDTO : AttributeResponseDTO
    {
        public bool IsNew { get; set; }
        public bool ISmart { get; set; }
        public string TVBrandId { get; set; }
        public string TVBrand { get; set; }
        public string TVTypeId { get; set; }
        public string TVType { get; set; }
        public string ScreenDiagonalId { get; set; }
        public string ScreenDiagonal { get; set; }
        public string ScreenResolutionId { get; set; }
        public string ScreenResolution { get; set; }
    }
}
