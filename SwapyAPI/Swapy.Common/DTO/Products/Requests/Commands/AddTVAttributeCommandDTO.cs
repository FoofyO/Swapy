using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class AddTVAttributeCommandDTO : AddProductCommandDTO<TVAttribute>
    {
        public bool isNew { get; set; }
        public bool isSmart { get; set; }
        public string tvTypeId { get; set; }
        public string tvBrandId { get; set; }
        public string screenResolutionId { get; set; }
        public string screenDiagonalId { get; set; }
    }
}
