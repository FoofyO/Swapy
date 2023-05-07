using MediatR;
using Swapy.BLL.CQRS.Commands;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class RemoveElectronicAttributeCommandHandler : IRequestHandler<RemoveElectronicAttributeCommand, Unit>
    {
        private readonly Guid _userId;
        private readonly IProductRepository _productRepository;
        private readonly IElectronicAttributeRepository _electronicAttributeRepository;

        public RemoveElectronicAttributeCommandHandler(Guid userId, IProductRepository productRepository, IElectronicAttributeRepository electronicAttributeRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _electronicAttributeRepository = electronicAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveElectronicAttributeCommand request, CancellationToken cancellationToken)
        {
            ElectronicAttribute electronicAttribute = await _electronicAttributeRepository.GetByIdAsync(request.ElectronicAttribute);
            Product product = await _productRepository.GetByIdAsync(electronicAttribute.ProductId);

            if (_userId != product.UserId) throw new NoAccessException("No access to uninstall this product.");

            await _electronicAttributeRepository.DeleteAsync(electronicAttribute);

            return Unit.Value;
        }
    }
}
