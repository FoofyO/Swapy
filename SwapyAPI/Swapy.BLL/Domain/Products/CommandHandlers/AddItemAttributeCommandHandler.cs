using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.BLL.Interfaces;
using Swapy.BLL.Services;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class AddItemAttributeCommandHandler : IRequestHandler<AddItemAttributeCommand, ItemAttribute>
    {
        private readonly string _userId;
        private readonly IProductRepository _productRepository;
        private readonly IItemAttributeRepository _itemAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public AddItemAttributeCommandHandler(string userId, IProductRepository productRepository, IItemAttributeRepository itemAttributeRepository, ISubcategoryRepository subcategoryRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _itemAttributeRepository = itemAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<ItemAttribute> Handle(AddItemAttributeCommand request, CancellationToken cancellationToken)
        {
            ISubcategoryService subcategoryService = new SubcategoryService(_subcategoryRepository);
            if (!await subcategoryService.SubcategoryValidationAsync(request.SubcategoryId)) throw new ArgumentException("Invalid subcategory.");

            Product product = new Product(request.Title, request.Description, request.Price, _userId, request.CurrencyId, request.CategoryId, request.SubcategoryId, request.CityId);
            await _productRepository.CreateAsync(product);

            ItemAttribute itemAttribute = new ItemAttribute(request.IsNew, request.ItemTypeId, product.Id);
            await _itemAttributeRepository.CreateAsync(itemAttribute);

            return itemAttribute;
        }
    }
}
