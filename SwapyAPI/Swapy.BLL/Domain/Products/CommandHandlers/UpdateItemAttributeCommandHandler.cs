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

            var itemAttribute = await _itemAttributeRepository.GetByIdAsync(request.ItemAttributeId);
            var product = await _productRepository.GetByIdAsync(itemAttribute.ProductId);

            if (!_userId.Equals(product.UserId)) throw new NoAccessException("No access to update this product");

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
