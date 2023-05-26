namespace Swapy.Common.DTO.Shops.Requests
{
    public class UpdateShopCommandDTO
    {
        public string shopName { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string slogan { get; set; }
        public string banner { get; set; }
        public string workDays { get; set; }
        public TimeSpan? startWorkTime { get; set; }
        public TimeSpan? endWorkTime { get; set; }
    }
}
