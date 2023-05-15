using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class RemoveTVAttributeCommandHandler : IRequestHandler<RemoveTVAttributeCommand, Unit>
    {
        private readonly string _userId;
        private readonly IProductRepository _productRepository;
        private readonly ITVAttributeRepository _tvAttributeRepository;

        public RemoveTVAttributeCommandHandler(string userId, IProductRepository productRepository, ITVAttributeRepository tvAttributeRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _tvAttributeRepository = tvAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveTVAttributeCommand request, CancellationToken cancellationToken)
        {
            var tvAttribute = await _tvAttributeRepository.GetByIdAsync(request.TVAttributeId);
            var product = await _productRepository.GetByIdAsync(tvAttribute.ProductId);

            if (!_userId.Equals(product.UserId)) throw new NoAccessException("No access to delete this product.");

            await _tvAttributeRepository.DeleteAsync(tvAttribute);

            return Unit.Value;
        }
    }
}
