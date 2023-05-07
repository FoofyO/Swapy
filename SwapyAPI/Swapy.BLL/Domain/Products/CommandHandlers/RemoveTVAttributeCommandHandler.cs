using MediatR;
using Swapy.BLL.CQRS.Commands;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class RemoveTVAttributeCommandHandler : IRequestHandler<RemoveTVAttributeCommand, Unit>
    {
        private readonly Guid _userId;
        private readonly IProductRepository _productRepository;
        private readonly ITVAttributeRepository _tvAttributeRepository;

        public RemoveTVAttributeCommandHandler(Guid userId, IProductRepository productRepository, ITVAttributeRepository tvAttributeRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _tvAttributeRepository = tvAttributeRepository;
        }

        public async Task<Unit> Handle(RemoveTVAttributeCommand request, CancellationToken cancellationToken)
        {
            TVAttribute tvAttribute = await _tvAttributeRepository.GetByIdAsync(request.TVAttributeId);
            Product product = await _productRepository.GetByIdAsync(tvAttribute.ProductId);

            if (_userId != product.UserId) throw new NoAccessException("No access to uninstall this product.");

            await _tvAttributeRepository.DeleteAsync(tvAttribute);

            return Unit.Value;
        }
    }
}
