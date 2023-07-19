using Microsoft.AspNetCore.Http;

namespace Swapy.Common.DTO.Shops.Requests
{
    public class UploadBannerCommandDTO
    {
        public IFormFile Banner { get; set; }
    }
}
