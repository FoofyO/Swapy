using Microsoft.AspNetCore.Http;

namespace Swapy.API
{
    public class UploadLogoCommandDTO
    {
        public IFormFile Logo { get; set; }
    }
}
