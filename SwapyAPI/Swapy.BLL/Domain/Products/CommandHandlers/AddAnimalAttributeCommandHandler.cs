using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.BLL.Interfaces;
using Swapy.BLL.Services;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    internal class AddAnimalAttributeCommandHandler : IRequestHandler<AddAnimalAttributeCommand, Unit>
    {
        private readonly Guid _userId;
        private readonly IProductRepository _productRepository;
        private readonly IAnimalAttributeRepository _animalAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public AddAnimalAttributeCommandHandler(Guid userId, IProductRepository productRepository, IAnimalAttributeRepository animalAttributeRepository, ISubcategoryRepository subcategoryRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _animalAttributeRepository = animalAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<Unit> Handle(AddAnimalAttributeCommand request, CancellationToken cancellationToken)
        {
            ISubcategoryService subcategoryService = new SubcategoryService(_subcategoryRepository);
            if (!await subcategoryService.SubcategoryValidationAsync(request.SubcategoryId)) throw new ArgumentException("Invalid subcategory.");

            Product product = new Product(request.Title, request.Description, request.Price, _userId, request.CurrencyId, request.CategoryId, request.SubcategoryId, request.CityId);
            await _productRepository.CreateAsync(product);

            AnimalAttribute animalAttribute = new AnimalAttribute(request.AnimalBreedId, product.Id);
            await _animalAttributeRepository.CreateAsync(animalAttribute);

            return Unit.Value;
        }
    }
}
