﻿using MediatR;
using Swapy.BLL.Domain.Animals.Commands;
using Swapy.BLL.Interfaces;
using Swapy.BLL.Services;
using Swapy.Common.Entities;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Animals.CommandHandlers
{
    public class AddAnimalAttributeCommandHandler : IRequestHandler<AddAnimalAttributeCommand, AnimalAttribute>
    {
        private readonly IImageService _imageService;
        private readonly IProductRepository _productRepository;
        private readonly INotificationService _notificationService;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IAnimalAttributeRepository _animalAttributeRepository;

        public AddAnimalAttributeCommandHandler(IImageService imageService, IProductRepository productRepository, INotificationService notificationService, ISubcategoryRepository subcategoryRepository, IAnimalAttributeRepository animalAttributeRepository)
        {
            _imageService = imageService;
            _productRepository = productRepository;
            _notificationService = notificationService;
            _subcategoryRepository = subcategoryRepository;
            _animalAttributeRepository = animalAttributeRepository;
        }

        public async Task<AnimalAttribute> Handle(AddAnimalAttributeCommand request, CancellationToken cancellationToken)
        {
            var subcategoryService = new SubcategoryService(_subcategoryRepository);
            if (!await subcategoryService.SubcategoryValidationAsync(request.SubcategoryId)) throw new ArgumentException("Invalid subcategory");

            Product product = new Product(request.Title, request.Description, request.Price, request.UserId, request.CurrencyId, request.CategoryId, request.SubcategoryId, request.CityId);
            await _productRepository.CreateAsync(product);

            AnimalAttribute animalAttribute = new AnimalAttribute(request.AnimalBreedId, product.Id);
            await _animalAttributeRepository.CreateAsync(animalAttribute);

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

            return animalAttribute;
        }
    }
}
