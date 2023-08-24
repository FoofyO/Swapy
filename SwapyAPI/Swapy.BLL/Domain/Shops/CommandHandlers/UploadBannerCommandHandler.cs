using MediatR;
using Swapy.BLL.Domain.Shops.Commands;
using Swapy.BLL.Interfaces;

namespace Swapy.BLL.Domain.Shops.CommandHandlers
{
    public class UploadBannerCommandHandler : IRequestHandler<UploadBannerCommand, Unit>
    {
        private readonly IImageService _imageService;

        public UploadBannerCommandHandler(IImageService imageService) => _imageService = imageService;

        public async Task<Unit> Handle(UploadBannerCommand request, CancellationToken cancellationToken)
        {
            return await _imageService.UploadBanner(request.Banner, request.UserId);
        }
    }
}
