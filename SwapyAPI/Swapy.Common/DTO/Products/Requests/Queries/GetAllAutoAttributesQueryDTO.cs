using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Queries
{
    public class GetAllAutoAttributesQueryDTO : GetAllProductQueryDTO<AutoAttribute>
    {
        public int? miliageMin { get; set; }
        public int? miliageMax { get; set; }
        public int? engineCapacityMin { get; set; }
        public int? engineCapacityMax { get; set; }
        public DateTime? releaseYearOlder { get; set; }
        public DateTime? releaseYearNewer { get; set; }
        public bool? isNew { get; set; }
        public List<string> fuelTypesId { get; set; }
        public List<string> autoColorsId { get; set; }
        public List<string> transmissionTypesId { get; set; }
        public List<string> autoBrandsId { get; set; }
        public List<string> autoTypesId { get; set; }
    }
}
