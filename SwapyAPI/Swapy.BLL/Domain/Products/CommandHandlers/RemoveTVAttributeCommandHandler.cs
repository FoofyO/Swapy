using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class RemoveTVAttributeCommandHandler : IRequestHandler<RemoveTVAttributeCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly ITVAttributeRepository _tvAttributeRepository;

        public RemoveTVAttributeCommandHandler(IProductRepository productRepository, ITVAttributeRepository tvAttributeRepository)
        {
            _productRepository = productRepository;
            _tvAttributeRepository = tvAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveTVAttributeCommand request, CancellationToken cancellationToken)
        {
            var tvAttribute = await _tvAttributeRepository.GetByIdAsync(request.TVAttributeId);
            var product = await _productRepository.GetByIdAsync(tvAttribute.ProductId);

            if (request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to delete this product");

            await _tvAttributeRepository.DeleteAsync(tvAttribute);

            return Unit.Value;
        }
    }
}
