﻿using MediatR;
using Swapy.BLL.Domain.Electronics.Commands;
using Swapy.BLL.Interfaces;
using Swapy.BLL.Services;
using Swapy.Common.Entities;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Electronics.CommandHandlers
{
    public class AddElectronicAttributeCommandHandler : IRequestHandler<AddElectronicAttributeCommand, ElectronicAttribute>
    {
        private readonly IImageService _imageService;
        private readonly IProductRepository _productRepository;
        private readonly INotificationService _notificationService;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IElectronicAttributeRepository _electronicAttributeRepository;

        public AddElectronicAttributeCommandHandler(IImageService imageService, IProductRepository productRepository, INotificationService notificationService, ISubcategoryRepository subcategoryRepository, IElectronicAttributeRepository electronicAttributeRepository)
        {
            _imageService = imageService;
            _productRepository = productRepository;
            _notificationService = notificationService;
            _subcategoryRepository = subcategoryRepository;
            _electronicAttributeRepository = electronicAttributeRepository;
        }

        public async Task<ElectronicAttribute> Handle(AddElectronicAttributeCommand request, CancellationToken cancellationToken)
        {
            ISubcategoryService subcategoryService = new SubcategoryService(_subcategoryRepository);
            if (!await subcategoryService.SubcategoryValidationAsync(request.SubcategoryId)) throw new ArgumentException("Invalid subcategory");

            Product product = new Product(request.Title, request.Description, request.Price, request.UserId, request.CurrencyId, request.CategoryId, request.SubcategoryId, request.CityId);
            await _productRepository.CreateAsync(product);

            ElectronicAttribute electronicAttribute = new ElectronicAttribute(request.IsNew, request.MemoryModelId, request.ModelColorId, product.Id);
            await _electronicAttributeRepository.CreateAsync(electronicAttribute);

            if (request.Files.Count > 0) await _imageService.UploadImages(request.Files, product.Id);

            var model = new NotificationModel()
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                CityId = request.CityId,
                Price = request.Price,
                CurrencyId = request.CurrencyId,
                ProductId = product.Id
            };

            await _notificationService.Notificate(model);

            return electronicAttribute;
        }
    }
}
