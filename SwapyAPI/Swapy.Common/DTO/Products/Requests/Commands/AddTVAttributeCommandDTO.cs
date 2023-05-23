using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class AddTVAttributeCommandDTO : AddProductCommandDTO<TVAttribute>
    {
        public bool IsNew { get; set; }
        public bool IsSmart { get; set; }
        public string TVTypeId { get; set; }
        public string TVBrandId { get; set; }
        public string ScreenResolutionId { get; set; }
        public string ScreenDiagonalId { get; set; }
    }
}
