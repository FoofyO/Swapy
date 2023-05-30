namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class UpdateRealEstateAttributeCommandDTO : UpdateProductCommandDTO
    {
        public int area { get; set; }
        public int rooms { get; set; }
        public bool isRent { get; set; }
        public string realEstateTypeId { get; set; }
    }
}
