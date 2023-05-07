using MediatR;
using Swapy.BLL.CQRS.Commands;
using Swapy.BLL.Interfaces;
using Swapy.BLL.Services;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.CQRS.CommandHandlers
{
    public class AddItemAttributeCommandHandler : IRequestHandler<AddItemAttributeCommand, Unit>
    {
        private readonly Guid _userId;
        private readonly IProductRepository _productRepository;
        private readonly IItemAttributeRepository _itemAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public AddItemAttributeCommandHandler(Guid userId, IProductRepository productRepository, IItemAttributeRepository itemAttributeRepository, ISubcategoryRepository subcategoryRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _itemAttributeRepository = itemAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<Unit> Handle(AddItemAttributeCommand request, CancellationToken cancellationToken)
        {
            ISubcategoryService subcategoryService = new SubcategoryService(_subcategoryRepository);
            if (!await subcategoryService.SubcategoryValidationAsync(request.SubcategoryId)) throw new ArgumentException("Invalid subcategory.");

            Product product = new Product(request.Title, request.Description, request.Price, _userId, request.CurrencyId, request.CategoryId, request.SubcategoryId, request.CityId);
            await _productRepository.CreateAsync(product);

            ItemAttribute itemAttribute = new ItemAttribute(request.IsNew, request.ItemTypeId, product.Id);
            await _itemAttributeRepository.CreateAsync(itemAttribute);

            return Unit.Value;
        }
    }
}
