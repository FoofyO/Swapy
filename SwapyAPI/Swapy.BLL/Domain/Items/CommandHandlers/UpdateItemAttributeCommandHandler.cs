using MediatR;
using Swapy.BLL.Domain.Items.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Items.CommandHandlers
{
    public class UpdateItemAttributeCommandHandler : IRequestHandler<UpdateItemAttributeCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IItemAttributeRepository _itemAttributeRepository;

        public UpdateItemAttributeCommandHandler(IProductRepository productRepository, IItemAttributeRepository itemAttributeRepository)
        {
            _productRepository = productRepository;
            _itemAttributeRepository = itemAttributeRepository;
        }

        public async Task<Unit> Handle(UpdateItemAttributeCommand request, CancellationToken cancellationToken)
        {

            var itemAttribute = await _itemAttributeRepository.GetByProductIdAsync(request.ProductId);
            var product = await _productRepository.GetByIdAsync(itemAttribute.ProductId);

            if (!request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to update this product");

            if (!string.IsNullOrEmpty(request.Title)) product.Title = request.Title;
            if (!string.IsNullOrEmpty(request.Description)) product.Description = request.Description;
            if (request.Price != null) product.Price = (decimal)request.Price;
            if (!string.IsNullOrEmpty(request.CurrencyId)) product.CurrencyId = request.CurrencyId;
            if (!string.IsNullOrEmpty(request.CategoryId)) product.CategoryId = request.CategoryId;
            if (!string.IsNullOrEmpty(request.SubcategoryId)) product.SubcategoryId = request.SubcategoryId;
            if (!string.IsNullOrEmpty(request.CityId)) product.CityId = request.CityId;
            await _productRepository.UpdateAsync(product);

            if (request.IsNew != null) itemAttribute.IsNew = (bool)request.IsNew;
            if (!string.IsNullOrEmpty(request.ItemTypeId)) itemAttribute.ItemTypeId = request.ItemTypeId;
            await _itemAttributeRepository.UpdateAsync(itemAttribute);

            return Unit.Value;
        }
    }
}
