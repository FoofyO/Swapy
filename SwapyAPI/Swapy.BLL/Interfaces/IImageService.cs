using MediatR;
using Microsoft.AspNetCore.Http;

namespace Swapy.BLL.Interfaces
{
    public interface IImageService
    {
        Task<Unit> UploadLogo(IFormFile file, string userId);
        Task<Unit> UploadBanner(IFormFile file, string userId);
        Task<Unit> UploadImages(IFormFileCollection files, string productId);
        Task<Unit> RemoveImages(List<string> paths, string productId);
    }
}
