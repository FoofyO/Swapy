using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class UpdateAnimalAttributeCommandHandler : IRequestHandler<UpdateAnimalAttributeCommand, Unit>
    {
        private readonly Guid _userId;
        private readonly IProductRepository _productRepository;
        private readonly IAnimalAttributeRepository _animalAttributeRepository;

        public UpdateAnimalAttributeCommandHandler(Guid userId, IProductRepository productRepository, IAnimalAttributeRepository animalAttributeRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _animalAttributeRepository = animalAttributeRepository;
        }

        public async Task<Unit> Handle(UpdateAnimalAttributeCommand request, CancellationToken cancellationToken)
        {
            AnimalAttribute animalAttribute;
            Product product;
            try
            {
                animalAttribute = await _animalAttributeRepository.GetByIdAsync(request.AnimalAttributeId);
                product = await _productRepository.GetByIdAsync(animalAttribute.ProductId);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException("Products not found.");
            }

            if (_userId != product.UserId) throw new NoAccessException("No access to uninstall this product.");

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
