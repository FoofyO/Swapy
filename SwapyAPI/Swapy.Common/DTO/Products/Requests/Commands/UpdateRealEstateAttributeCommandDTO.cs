namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class UpdateRealEstateAttributeCommandDTO : UpdateProductCommandDTO
    {
        public string RealEstateAttributeId { get; set; }
        public int Area { get; set; }
        public int Rooms { get; set; }
        public bool IsRent { get; set; }
        public string RealEstateTypeId { get; set; }
    }
}
