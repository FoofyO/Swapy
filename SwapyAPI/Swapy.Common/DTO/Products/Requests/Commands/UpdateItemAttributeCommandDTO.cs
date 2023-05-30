namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class UpdateItemAttributeCommandDTO : UpdateProductCommandDTO
    {
        public bool isNew { get; set; }
        public string itemTypeId { get; set; }
    }
}
