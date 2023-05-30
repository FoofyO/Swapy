﻿using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class UpdateTVAttributeCommandHandler : IRequestHandler<UpdateTVAttributeCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly ITVAttributeRepository _tvAttributeRepository;

        public UpdateTVAttributeCommandHandler(IProductRepository productRepository, ITVAttributeRepository tvAttributeRepository)
        {
            _productRepository = productRepository;
            _tvAttributeRepository = tvAttributeRepository;
        }

        public async Task<Unit> Handle(UpdateTVAttributeCommand request, CancellationToken cancellationToken)
        {
            var tvAttribute = await _tvAttributeRepository.GetByProductIdAsync(request.ProductId);
            var product = await _productRepository.GetByIdAsync(tvAttribute.ProductId);

            if (!request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to update this product");

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
