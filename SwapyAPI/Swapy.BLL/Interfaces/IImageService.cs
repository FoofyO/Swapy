using MediatR;
using Microsoft.AspNetCore.Http;

namespace Swapy.BLL.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadChatImagesAsync(IFormFile file);
        Task<Unit> UploadLogoAsync(IFormFile file, string userId);
        Task<Unit> UploadBannerAsync(IFormFile file, string userId);
        Task<Unit> UploadProductImagesAsync(IFormFileCollection files, string productId);
        Task<Unit> RemoveProductImagesAsync(List<string> paths, string productId);
    }
}
