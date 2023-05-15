using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class RemoveAnimalAttributeCommandHandler : IRequestHandler<RemoveAnimalAttributeCommand, Unit>
    {
        private readonly string _userId;
        private readonly IProductRepository _productRepository;
        private readonly IAnimalAttributeRepository _animalAttributeRepository;

        public RemoveAnimalAttributeCommandHandler(string userId, IProductRepository productRepository, IAnimalAttributeRepository animalAttributeRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _animalAttributeRepository = animalAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveAnimalAttributeCommand request, CancellationToken cancellationToken)
        {
            var animalAttribute = await _animalAttributeRepository.GetByIdAsync(request.AnimalAttributeId);
            var product = await _productRepository.GetByIdAsync(animalAttribute.ProductId);

            if (!_userId.Equals(product.UserId)) throw new NoAccessException("No access to delete this product.");

            await _animalAttributeRepository.DeleteAsync(animalAttribute);

            return Unit.Value;
        }
    }
}
