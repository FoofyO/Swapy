namespace Swapy.Common.DTO.Products.Responses
{
    public class RealEstateAttributeResponseDTO : AttributeResponseDTO
    {
        public int Area { get; set; }
        public int Rooms { get; set; }
        public bool IsRent { get; set; }
    }
}
