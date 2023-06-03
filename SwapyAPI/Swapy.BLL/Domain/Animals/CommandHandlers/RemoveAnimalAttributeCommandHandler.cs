using MediatR;
using Swapy.BLL.Domain.Animals.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Animals.CommandHandlers
{
    public class RemoveAnimalAttributeCommandHandler : IRequestHandler<RemoveAnimalAttributeCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IAnimalAttributeRepository _animalAttributeRepository;

        public RemoveAnimalAttributeCommandHandler(IProductRepository productRepository, IAnimalAttributeRepository animalAttributeRepository)
        {
            _productRepository = productRepository;
            _animalAttributeRepository = animalAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveAnimalAttributeCommand request, CancellationToken cancellationToken)
        {
            var animalAttribute = await _animalAttributeRepository.GetByIdAsync(request.AnimalAttributeId);
            var product = await _productRepository.GetByIdAsync(animalAttribute.ProductId);

            if (request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to delete this product");

            await _animalAttributeRepository.DeleteAsync(animalAttribute);

            return Unit.Value;
        }
    }
}
