using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllElectronicAttributeQuery : GetAllProductQuery<ElectronicAttribute>
    {
        public bool? IsNew { get; set; }
        public List<Guid> MemoriesId { get; set; }
        public List<Guid> ColorsId { get; set; }
        public List<Guid> ModelsId { get; set; }
        public List<Guid> BrandsId { get; set; }
        public List<Guid> TypesId { get; set; }
    }
}
