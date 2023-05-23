﻿using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class UpdateAnimalAttributeCommandHandler : IRequestHandler<UpdateAnimalAttributeCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IAnimalAttributeRepository _animalAttributeRepository;

        public UpdateAnimalAttributeCommandHandler(IProductRepository productRepository, IAnimalAttributeRepository animalAttributeRepository)
        {
            _productRepository = productRepository;
            _animalAttributeRepository = animalAttributeRepository;
        }

        public async Task<Unit> Handle(UpdateAnimalAttributeCommand request, CancellationToken cancellationToken)
        {
            var animalAttribute = await _animalAttributeRepository.GetByIdAsync(request.AnimalAttributeId);
            var product = await _productRepository.GetByIdAsync(animalAttribute.ProductId);

            if (!request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to update this product");

            product.Title = request.Title;
            product.Description = request.Description;
            product.Price = request.Price;
            product.CurrencyId = request.CurrencyId;
            product.CategoryId = request.CategoryId;
            product.SubcategoryId = request.SubcategoryId;
            product.CityId = request.CityId;
            await _productRepository.UpdateAsync(product);

            animalAttribute.AnimalBreedId = request.AnimalBreedId;
            await _animalAttributeRepository.UpdateAsync(animalAttribute);

            return Unit.Value;
        }
    }
}
