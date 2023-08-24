using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Services
{
    public class ImageService : IImageService
    {
        private readonly UserManager<User> _userManager;
        private readonly IKeyVaultService _keyVaultService;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IShopAttributeRepository _shopAttributeRepository;

        public ImageService(UserManager<User> userManager, IKeyVaultService keyVaultService, IProductImageRepository productImageRepository, IShopAttributeRepository shopAttributeRepository)
        {
            _userManager = userManager;
            _keyVaultService = keyVaultService;
            _productImageRepository = productImageRepository;
            _shopAttributeRepository = shopAttributeRepository;
        }

        public async Task<Unit> UploadLogoAsync(IFormFile file, string userId)
        {
            var blobUrl = await _keyVaultService.GetSecretValue("BlobStorage");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var blobServiceClient = new BlobServiceClient(blobUrl);
            var containerClient = blobServiceClient.GetBlobContainerClient("logos");

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            fileExtension = fileExtension.Substring(1);

            await containerClient.UploadBlobAsync(fileName, file.OpenReadStream());

            var blobClient = containerClient.GetBlobClient(fileName);

            var blobHttpHeaders = new BlobHttpHeaders
            {
                ContentType = "image/" + fileExtension,
                ContentDisposition = "inline; filename=\"" + fileName + "\""
            };

            await blobClient.SetHttpHeadersAsync(blobHttpHeaders);

            var user = await _userManager.FindByIdAsync(userId);
            user.Logo = fileName;
            await _userManager.UpdateAsync(user);

            return Unit.Value;
        }

        public async Task<Unit> UploadBannerAsync(IFormFile file, string userId)
        {
            var blobUrl = await _keyVaultService.GetSecretValue("BlobStorage");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var blobServiceClient = new BlobServiceClient(blobUrl);
            var containerClient = blobServiceClient.GetBlobContainerClient("banners");

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            fileExtension = fileExtension.Substring(1);

            await containerClient.UploadBlobAsync(fileName, file.OpenReadStream());
            
            var blobClient = containerClient.GetBlobClient(fileName);

            var blobHttpHeaders = new BlobHttpHeaders
            {
                ContentType = "image/" + fileExtension,
                ContentDisposition = "inline; filename=\"" + fileName + "\""
            };

            await blobClient.SetHttpHeadersAsync(blobHttpHeaders);

            var shop = await _shopAttributeRepository.GetByUserIdAsync(userId);
            shop.Banner = fileName;
            await _shopAttributeRepository.UpdateAsync(shop);

            return Unit.Value;
        }

        public async Task<string> UploadChatImagesAsync(IFormFile file)
        {
            var blobUrl = await _keyVaultService.GetSecretValue("BlobStorage");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var blobServiceClient = new BlobServiceClient(blobUrl);
            var containerClient = blobServiceClient.GetBlobContainerClient("messages");

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            fileExtension = fileExtension.Substring(1);

            await containerClient.UploadBlobAsync(fileName, file.OpenReadStream());

            var blobClient = containerClient.GetBlobClient(fileName);

            var blobHttpHeaders = new BlobHttpHeaders
            {
                ContentType = "image/" + fileExtension,
                ContentDisposition = "inline; filename=\"" + fileName + "\""
            };

            await blobClient.SetHttpHeadersAsync(blobHttpHeaders);

            return fileName;
        }

        public async Task<Unit> UploadProductImagesAsync(IFormFileCollection files, string productId)
        {
            var blob = await _keyVaultService.GetSecretValue("BlobStorage");

            foreach (var image in files)
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var blobServiceClient = new BlobServiceClient(blob);
                var containerClient = blobServiceClient.GetBlobContainerClient("product-images");

                var fileExtension = Path.GetExtension(image.FileName).ToLower();
                fileExtension = fileExtension.Substring(1);

                await containerClient.UploadBlobAsync(imageName, image.OpenReadStream());

                var blobClient = containerClient.GetBlobClient(imageName);

                var blobHttpHeaders = new BlobHttpHeaders
                {
                    ContentType = "image/" + fileExtension,
                    ContentDisposition = "inline; filename=\"" + imageName + "\""
                };

                await blobClient.SetHttpHeadersAsync(blobHttpHeaders);

                var productImage = new ProductImage(imageName, productId);
                await _productImageRepository.CreateAsync(productImage);
            }
            
            return Unit.Value;
        }

        public async Task<Unit> RemoveProductImagesAsync(List<string> paths, string productId)
        {
            foreach (var item in paths)
            {
                var tmpProductImage = await _productImageRepository.GetByPath(item, productId);
                if (tmpProductImage != null) await _productImageRepository.DeleteAsync(tmpProductImage);
            }

            return Unit.Value;
        }
    }
}
