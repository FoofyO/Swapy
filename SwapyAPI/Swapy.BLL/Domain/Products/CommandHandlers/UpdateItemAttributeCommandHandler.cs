using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class UpdateItemAttributeCommandHandler : IRequestHandler<UpdateItemAttributeCommand, Unit>
    {
        private readonly string _userId;
        private readonly IProductRepository _productRepository;
        private readonly IItemAttributeRepository _itemAttributeRepository;

        public UpdateItemAttributeCommandHandler(string userId, IProductRepository productRepository, IItemAttributeRepository itemAttributeRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _itemAttributeRepository = itemAttributeRepository;
        }
        
        public async Task<Unit> Handle(UpdateItemAttributeCommand request, CancellationToken cancellationToken)
        {
            ItemAttribute itemAttribute;
            Product product;
            try
            {
                itemAttribute = await _itemAttributeRepository.GetByIdAsync(request.ItemAttributeId);
                product = await _productRepository.GetByIdAsync(itemAttribute.ProductId);
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

            itemAttribute.IsNew = request.IsNew;
            itemAttribute.ItemTypeId = request.ItemTypeId;
            await _itemAttributeRepository.UpdateAsync(itemAttribute);

            return Unit.Value;
        }
    }
}
