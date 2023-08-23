using Azure.Storage.Blobs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Services
{
    public class ImageService : IImageService
    {
        private readonly IKeyVaultService _keyVaultService;
        private readonly IProductImageRepository _productImageRepository;

        public ImageService(IKeyVaultService keyVaultService, IProductImageRepository productImageRepository)
        {
            _keyVaultService = keyVaultService;
            _productImageRepository = productImageRepository;
        }

        public async Task<Unit> UploadImages(IFormFileCollection files, string productId)
        {
            var blob = await _keyVaultService.GetSecretValue("BlobStorage");

            foreach (var image in files)
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var blobServiceClient = new BlobServiceClient(blob);
                var containerClient = blobServiceClient.GetBlobContainerClient("product-images");

                await containerClient.UploadBlobAsync(imageName, image.OpenReadStream());
                
                var productImage = new ProductImage(imageName, productId);
                await _productImageRepository.CreateAsync(productImage);
            }
            
            return Unit.Value;
        }

        public async Task<Unit> RemoveImages(List<string> paths, string productId)
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
