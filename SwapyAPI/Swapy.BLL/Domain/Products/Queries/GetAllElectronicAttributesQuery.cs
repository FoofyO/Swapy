using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllElectronicAttributesQuery : GetAllProductQuery<ElectronicAttribute>
    {
        public bool? IsNew { get; set; }
        public List<string> MemoriesId { get; set; }
        public List<string> ColorsId { get; set; }
        public List<string> ModelsId { get; set; }
        public List<string> BrandsId { get; set; }
        public List<string> TypesId { get; set; }
    }
}
