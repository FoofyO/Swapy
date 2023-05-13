using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class UpdateAutoAttributeCommandHandler : IRequestHandler<UpdateAutoAttributeCommand, Unit>
    {
        private readonly string _userId;
        private readonly IProductRepository _productRepository;
        private readonly IAutoAttributeRepository _autoAttributeRepository;

        public UpdateAutoAttributeCommandHandler(string userId, IProductRepository productRepository, IAutoAttributeRepository autoAttributeRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _autoAttributeRepository = autoAttributeRepository;
        }

        public async Task<Unit> Handle(UpdateAutoAttributeCommand request, CancellationToken cancellationToken)
        {
            var autoAttribute = await _autoAttributeRepository.GetByIdAsync(request.AutoAttributeId);
            var product = await _productRepository.GetByIdAsync(autoAttribute.ProductId);

            if (_userId != product.UserId) throw new NoAccessException("No access to uninstall this product");

            product.Title = request.Title;
            product.Description = request.Description;
            product.Price = request.Price;
            product.CurrencyId = request.CurrencyId;
            product.CategoryId = request.CategoryId;
            product.SubcategoryId = request.SubcategoryId;
            product.CityId = request.CityId;
            await _productRepository.UpdateAsync(product);

            autoAttribute.Miliage = request.Miliage;
            autoAttribute.EngineCapacity = request.EngineCapacity;
            autoAttribute.ReleaseYear = request.ReleaseYear;
            autoAttribute.IsNew = request.IsNew;
            autoAttribute.FuelTypeId = request.FuelTypeId;
            autoAttribute.AutoColorId = request.AutoColorId;
            autoAttribute.TransmissionTypeId = request.TransmissionTypeId;
            autoAttribute.AutoBrandTypeId = request.AutoBrandTypeId;
            await _autoAttributeRepository.UpdateAsync(autoAttribute);

            return Unit.Value;
        }
    }
}
