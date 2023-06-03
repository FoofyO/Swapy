using MediatR;
using Swapy.BLL.Domain.Electronics.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Electronics.CommandHandlers
{
    public class RemoveElectronicAttributeCommandHandler : IRequestHandler<RemoveElectronicAttributeCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IElectronicAttributeRepository _electronicAttributeRepository;

        public RemoveElectronicAttributeCommandHandler(IProductRepository productRepository, IElectronicAttributeRepository electronicAttributeRepository)
        {
            _productRepository = productRepository;
            _electronicAttributeRepository = electronicAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveElectronicAttributeCommand request, CancellationToken cancellationToken)
        {
            var electronicAttribute = await _electronicAttributeRepository.GetByIdAsync(request.ElectronicAttributeId);
            var product = await _productRepository.GetByIdAsync(electronicAttribute.ProductId);

            if (request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to delete this product");

            await _electronicAttributeRepository.DeleteAsync(electronicAttribute);

            return Unit.Value;
        }
    }
}
