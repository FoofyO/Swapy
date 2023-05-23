namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class UpdateItemAttributeCommandDTO : UpdateProductCommandDTO
    {
        public string ItemAttributeId { get; set; }
        public bool IsNew { get; set; }
        public string ItemTypeId { get; set; }
    }
}
