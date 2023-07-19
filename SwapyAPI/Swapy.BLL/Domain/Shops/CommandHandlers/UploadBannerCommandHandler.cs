using Azure.Storage.Blobs;
using MediatR;
using Swapy.BLL.Domain.Shops.Commands;
using Swapy.BLL.Interfaces;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Shops.CommandHandlers
{
    public class UploadBannerCommandHandler : IRequestHandler<UploadBannerCommand, Unit>
    {
        private readonly IKeyVaultService _keyVaultService;
        private readonly IShopAttributeRepository _shopAttributeRepository;

        public UploadBannerCommandHandler(IKeyVaultService keyVaultService, IShopAttributeRepository shopAttributeRepository)
        {
            _keyVaultService = keyVaultService;
            _shopAttributeRepository = shopAttributeRepository;
        }

        public async Task<Unit> Handle(UploadBannerCommand request, CancellationToken cancellationToken)
        {
            var blobUrl = await _keyVaultService.GetSecretValue("BlobStorage");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Banner.FileName);
            var blobServiceClient = new BlobServiceClient(blobUrl);
            var containerClient = blobServiceClient.GetBlobContainerClient("banners");

            await containerClient.UploadBlobAsync(fileName, request.Banner.OpenReadStream());

            var shop = await _shopAttributeRepository.GetByUserIdAsync(request.UserId);
            shop.Banner = fileName;
            await _shopAttributeRepository.UpdateAsync(shop);

            return Unit.Value;
        }
    }
}
