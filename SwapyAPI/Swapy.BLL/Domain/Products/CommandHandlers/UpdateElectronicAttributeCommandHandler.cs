using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class UpdateElectronicAttributeCommandHandler : IRequestHandler<UpdateElectronicAttributeCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IElectronicAttributeRepository _electronicAttributeRepository;

        public UpdateElectronicAttributeCommandHandler(IProductRepository productRepository, IElectronicAttributeRepository electronicAttributeRepository)
        {
            _productRepository = productRepository;
            _electronicAttributeRepository = electronicAttributeRepository;
        }

        public async Task<Unit> Handle(UpdateElectronicAttributeCommand request, CancellationToken cancellationToken)
        {
            var electronicAttribute = await _electronicAttributeRepository.GetByProductIdAsync(request.ProductId);
            var product = await _productRepository.GetByIdAsync(electronicAttribute.ProductId);

            if (!request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to update this product");

            product.Title = request.Title;
            product.Description = request.Description;
            product.Price = request.Price;
            product.CurrencyId = request.CurrencyId;
            product.CategoryId = request.CategoryId;
            product.SubcategoryId = request.SubcategoryId;
            product.CityId = request.CityId;
            await _productRepository.UpdateAsync(product);

            electronicAttribute.IsNew = request.IsNew;
            electronicAttribute.MemoryModelId = request.MemoryModelId;
            electronicAttribute.ModelColorId = request.ModelColorId;
            await _electronicAttributeRepository.UpdateAsync(electronicAttribute);

            return Unit.Value;
        }
    }
}
