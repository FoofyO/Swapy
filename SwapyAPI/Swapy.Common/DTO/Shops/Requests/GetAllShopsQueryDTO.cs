namespace Swapy.Common.DTO.Shops.Requests
{
    public class GetAllShopsQueryDTO
    {
        public string? title { get; set; }
        public int page { get; set; }
        public int pagesize { get; set; }
        public bool? sortbyviews { get; set; }
        public bool? reversesort { get; set; }
    }
}
