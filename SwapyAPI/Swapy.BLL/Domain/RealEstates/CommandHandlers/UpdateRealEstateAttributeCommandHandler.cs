using MediatR;
using Swapy.BLL.Domain.RealEstates.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.RealEstates.CommandHandlers
{
    public class UpdateRealEstateAttributeCommandHandler : IRequestHandler<UpdateRealEstateAttributeCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IRealEstateAttributeRepository _realEstateAttributeRepository;

        public UpdateRealEstateAttributeCommandHandler(IProductRepository productRepository, IRealEstateAttributeRepository realEstateAttributeRepository)
        {
            _productRepository = productRepository;
            _realEstateAttributeRepository = realEstateAttributeRepository;
        }

        public async Task<Unit> Handle(UpdateRealEstateAttributeCommand request, CancellationToken cancellationToken)
        {
            var realEstateAttribute = await _realEstateAttributeRepository.GetByProductIdAsync(request.ProductId);
            var product = await _productRepository.GetByIdAsync(realEstateAttribute.ProductId);

            if (!request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to update this product");

            if (!string.IsNullOrEmpty(request.Title)) product.Title = request.Title;
            if (!string.IsNullOrEmpty(request.Description)) product.Description = request.Description;
            if (request.Price != null) product.Price = (decimal)request.Price;
            if (!string.IsNullOrEmpty(request.CurrencyId)) product.CurrencyId = request.CurrencyId;
            if (!string.IsNullOrEmpty(request.CategoryId)) product.CategoryId = request.CategoryId;
            if (!string.IsNullOrEmpty(request.SubcategoryId)) product.SubcategoryId = request.SubcategoryId;
            if (!string.IsNullOrEmpty(request.CityId)) product.CityId = request.CityId;
            await _productRepository.UpdateAsync(product);

            if (request.Area != null) realEstateAttribute.Area = (int)request.Area;
            if (request.Rooms != null) realEstateAttribute.Rooms = (int)request.Rooms;
            if (request.IsRent != null) realEstateAttribute.IsRent = (bool)request.IsRent;
            if (!string.IsNullOrEmpty(request.RealEstateTypeId)) realEstateAttribute.RealEstateTypeId = request.RealEstateTypeId;
            await _realEstateAttributeRepository.UpdateAsync(realEstateAttribute);

            return Unit.Value;
        }
    }
}
