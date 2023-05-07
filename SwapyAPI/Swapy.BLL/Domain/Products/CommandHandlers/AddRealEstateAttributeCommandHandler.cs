using MediatR;
using Swapy.BLL.CQRS.Commands;
using Swapy.BLL.Interfaces;
using Swapy.BLL.Services;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;


namespace Swapy.BLL.CQRS.CommandHandlers
{
    public class AddRealEstateAttributeCommandHandler : IRequestHandler<AddRealEstateAttributeCommand, Unit>
    {
        private readonly Guid _userId;
        private readonly IProductRepository _productRepository;
        private readonly IRealEstateAttributeRepository _realEstateAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public AddRealEstateAttributeCommandHandler(Guid userId, IProductRepository productRepository, IRealEstateAttributeRepository realEstateAttributeRepository, ISubcategoryRepository subcategoryRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _realEstateAttributeRepository = realEstateAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<Unit> Handle(AddRealEstateAttributeCommand request, CancellationToken cancellationToken)
        {
            ISubcategoryService subcategoryService = new SubcategoryService(_subcategoryRepository);
            if (!await subcategoryService.SubcategoryValidationAsync(request.SubcategoryId)) throw new ArgumentException("Invalid subcategory.");

            Product product = new Product(request.Title, request.Description, request.Price, _userId, request.CurrencyId, request.CategoryId, request.SubcategoryId, request.CityId);
            await _productRepository.CreateAsync(product);

            RealEstateAttribute animalAttribute = new RealEstateAttribute(request.Area, request.Rooms, request.IsRent, request.RealEstateTypeId, product.Id);
            await _realEstateAttributeRepository.CreateAsync(animalAttribute);

            return Unit.Value;
        }
    }
}
