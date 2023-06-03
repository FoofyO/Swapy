using MediatR;
using Swapy.BLL.Domain.Electronics.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Electronics.CommandHandlers
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

            if (!string.IsNullOrEmpty(request.Title)) product.Title = request.Title;
            if (!string.IsNullOrEmpty(request.Description)) product.Description = request.Description;
            if (request.Price != null) product.Price = (decimal)request.Price;
            if (!string.IsNullOrEmpty(request.CurrencyId)) product.CurrencyId = request.CurrencyId;
            if (!string.IsNullOrEmpty(request.CategoryId)) product.CategoryId = request.CategoryId;
            if (!string.IsNullOrEmpty(request.SubcategoryId)) product.SubcategoryId = request.SubcategoryId;
            if (!string.IsNullOrEmpty(request.CityId)) product.CityId = request.CityId;
            await _productRepository.UpdateAsync(product);

            if (request.IsNew != null) electronicAttribute.IsNew = (bool)request.IsNew;
            if (!string.IsNullOrEmpty(request.MemoryModelId)) electronicAttribute.MemoryModelId = request.MemoryModelId;
            if (!string.IsNullOrEmpty(request.ModelColorId)) electronicAttribute.ModelColorId = request.ModelColorId;
            await _electronicAttributeRepository.UpdateAsync(electronicAttribute);

            return Unit.Value;
        }
    }
}
