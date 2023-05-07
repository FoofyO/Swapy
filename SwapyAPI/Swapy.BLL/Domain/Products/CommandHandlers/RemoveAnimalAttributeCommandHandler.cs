﻿using MediatR;
using Swapy.BLL.CQRS.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.CQRS.CommandHandlers
{
    public class RemoveAnimalAttributeCommandHandler : IRequestHandler<RemoveAnimalAttributeCommand, Unit>
    {
        private readonly Guid _userId;
        private readonly IProductRepository _productRepository;
        private readonly IAnimalAttributeRepository _animalAttributeRepository;

        public RemoveAnimalAttributeCommandHandler(Guid userId, IProductRepository productRepository, IAnimalAttributeRepository animalAttributeRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _animalAttributeRepository = animalAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveAnimalAttributeCommand request, CancellationToken cancellationToken)
        {
            AnimalAttribute animalAttribute = await _animalAttributeRepository.GetByIdAsync(request.AnimalAttributeId);
            Product product = await _productRepository.GetByIdAsync(animalAttribute.ProductId);

            if (_userId != product.UserId) throw new NoAccessException("No access to uninstall this product.");

            await _animalAttributeRepository.DeleteAsync(animalAttribute);

            return Unit.Value;
        }
    }
}
