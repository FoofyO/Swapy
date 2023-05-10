using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class UpdateRealEstateAttributeCommandHandler : IRequestHandler<UpdateRealEstateAttributeCommand, Unit>
    {
        private readonly string _userId;
        private readonly IProductRepository _productRepository;
        private readonly IRealEstateAttributeRepository _realEstateAttributeRepository;

        public UpdateRealEstateAttributeCommandHandler(string userId, IProductRepository productRepository, IRealEstateAttributeRepository realEstateAttributeRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _realEstateAttributeRepository = realEstateAttributeRepository;
        }

        public async Task<Unit> Handle(UpdateRealEstateAttributeCommand request, CancellationToken cancellationToken)
        {
            RealEstateAttribute realEstateAttribute;
            Product product;
            try
            {
                realEstateAttribute = await _realEstateAttributeRepository.GetByIdAsync(request.RealEstateAttributeId);
                product = await _productRepository.GetByIdAsync(realEstateAttribute.ProductId);
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

            realEstateAttribute.Area = request.Area;
            realEstateAttribute.Rooms = request.Rooms;
            realEstateAttribute.IsRent = request.IsRent;
            realEstateAttribute.RealEstateTypeId = request.RealEstateTypeId;
            await _realEstateAttributeRepository.UpdateAsync(realEstateAttribute);

            return Unit.Value;
        }
    }
}
