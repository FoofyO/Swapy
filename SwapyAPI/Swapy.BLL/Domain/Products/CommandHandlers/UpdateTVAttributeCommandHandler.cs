using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class UpdateTVAttributeCommandHandler : IRequestHandler<UpdateTVAttributeCommand, Unit>
    {
        private readonly Guid _userId;
        private readonly IProductRepository _productRepository;
        private readonly ITVAttributeRepository _tvAttributeRepository;

        public UpdateTVAttributeCommandHandler(Guid userId, IProductRepository productRepository, ITVAttributeRepository tvAttributeRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _tvAttributeRepository = tvAttributeRepository;
        }

        public async Task<Unit> Handle(UpdateTVAttributeCommand request, CancellationToken cancellationToken)
        {
            TVAttribute tvAttribute;
            Product product;
            try
            {
                tvAttribute = await _tvAttributeRepository.GetByIdAsync(request.TVAttributeId);
                product = await _productRepository.GetByIdAsync(tvAttribute.ProductId);
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

            tvAttribute.IsNew = request.IsNew;
            tvAttribute.IsSmart = request.IsSmart;
            tvAttribute.TVTypeId = request.TVTypeId;
            tvAttribute.TVBrandId = request.TVBrandId;
            tvAttribute.ScreenResolutionId = request.ScreenResolutionId;
            tvAttribute.ScreenDiagonalId = request.ScreenDiagonalId;
            await _tvAttributeRepository.UpdateAsync(tvAttribute);

            return Unit.Value;
        }
    }
}
