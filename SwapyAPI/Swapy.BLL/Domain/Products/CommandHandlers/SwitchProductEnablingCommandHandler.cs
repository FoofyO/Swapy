using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class SwitchProductEnablingCommandHandler : IRequestHandler<SwitchProductEnablingCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        
        public SwitchProductEnablingCommandHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<Unit> Handle(SwitchProductEnablingCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if (!request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to change enabling this product");

            product.IsDisable = !product.IsDisable;
            await _productRepository.UpdateAsync(product);

            return Unit.Value;
        }
    }
}
