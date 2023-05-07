using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllAutoAttributeQuery : GetAllProductQuery<AutoAttribute>
    {
        public int? MiliageMin { get; set; }
        public int? MiliageMax { get; set; }
        public int? EngineCapacityMin { get; set; }
        public int? EngineCapacityMax { get; set; }
        public DateTime? ReleaseYearOlder { get; set; }
        public DateTime? ReleaseYearNewer { get; set; }
        public bool? IsNew { get; set; }
        public List<Guid> FuelTypesId { get; set; }
        public List<Guid> AutoColorsId { get; set; }
        public List<Guid> TransmissionTypesId { get; set; }
        public List<Guid> AutoBrandsId { get; set; }
        public List<Guid> AutoTypesId { get; set; }
    }
}
