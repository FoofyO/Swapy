using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class UploadLogoCommandHandler : IRequestHandler<UploadLogoCommand, Unit>
    {
        private readonly IKeyVaultService _keyVaultService;
        private readonly UserManager<User> _userManager;

        public UploadLogoCommandHandler(IKeyVaultService keyVaultService, UserManager<User> userManager)
        {
            _keyVaultService = keyVaultService;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(UploadLogoCommand request, CancellationToken cancellationToken)
        {
            var blobUrl = await _keyVaultService.GetSecretValue("BlobStorage");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Logo.FileName);
            var blobServiceClient = new BlobServiceClient(blobUrl);
            var containerClient = blobServiceClient.GetBlobContainerClient("logos");

            var fileExtension = Path.GetExtension(request.Logo.FileName).ToLower();
            fileExtension = fileExtension.Substring(1);

            await containerClient.UploadBlobAsync(fileName, request.Logo.OpenReadStream());

            var blobClient = containerClient.GetBlobClient(fileName);

            var blobHttpHeaders = new BlobHttpHeaders
            {
                ContentType = "image/" + fileExtension,
                ContentDisposition = "inline; filename=\"" + fileName + "\""
            };

            await blobClient.SetHttpHeadersAsync(blobHttpHeaders);

            var user = await _userManager.FindByIdAsync(request.UserId);
            user.Logo = fileName;
            await _userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
