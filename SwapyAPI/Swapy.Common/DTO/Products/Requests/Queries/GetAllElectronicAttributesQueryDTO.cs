using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Queries
{
    public class GetAllElectronicAttributesQueryDTO : GetAllProductQueryDTO<ElectronicAttribute>
    {
        public bool? isNew { get; set; }
        public List<string> memoriesId { get; set; }
        public List<string> colorsId { get; set; }
        public List<string> modelsId { get; set; }
        public List<string> brandsId { get; set; }
        public List<string> typesId { get; set; }
    }
}
