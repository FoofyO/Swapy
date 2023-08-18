using MediatR;
using Microsoft.AspNetCore.Http;

namespace Swapy.BLL.Interfaces
{
    public interface IImageService
    {
        Task<Unit> UploadImages(IFormFileCollection files, string productId);
        Task<Unit> RemoveImages(List<string> paths, string productId);
    }
}
