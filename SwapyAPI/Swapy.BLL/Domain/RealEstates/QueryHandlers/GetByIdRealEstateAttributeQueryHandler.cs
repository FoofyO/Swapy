﻿using MediatR;
using Swapy.BLL.Domain.RealEstates.Queries;
using Swapy.Common.DTO.RealEstates.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.RealEstates.QueryHandlers
{
    public class GetByIdRealEstateAttributeQueryHandler : IRequestHandler<GetByIdRealEstateAttributeQuery, RealEstateAttributeResponseDTO>
    {
        private readonly IRealEstateAttributeRepository _realEstateAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetByIdRealEstateAttributeQueryHandler(IRealEstateAttributeRepository realEstateAttributeRepository, ISubcategoryRepository subcategoryRepository)
        {
            _realEstateAttributeRepository = realEstateAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<RealEstateAttributeResponseDTO> Handle(GetByIdRealEstateAttributeQuery request, CancellationToken cancellationToken)
        {
            var realEstateAttribute = await _realEstateAttributeRepository.GetDetailByIdAsync(request.ProductId);
            List<CategoryNode> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(realEstateAttribute.Product.SubcategoryId, request.Language)).ToList();
            categories.Insert(0, new CategoryNode(realEstateAttribute.Product.CategoryId, realEstateAttribute.Product.Category.Names.FirstOrDefault(l => l.Language == request.Language).Value));

            RealEstateAttributeResponseDTO result = new RealEstateAttributeResponseDTO()
            {
                Id = realEstateAttribute.Id,
                City = realEstateAttribute.Product.City.Names.FirstOrDefault(l => l.Language == request.Language).Value,
                Currency = realEstateAttribute.Product.Currency.Name,
                CurrencySymbol = realEstateAttribute.Product.Currency.Symbol,
                UserId = realEstateAttribute.Product.UserId,
                FirstName = realEstateAttribute.Product.User.FirstName,
                LastName = realEstateAttribute.Product.User.LastName,
                PhoneNumber = realEstateAttribute.Product.User.PhoneNumber,
                ShopId = realEstateAttribute.Product.User.ShopAttributeId,
                Shop = realEstateAttribute.Product.User.ShopAttribute.ShopName,
                UserType = realEstateAttribute.Product.User.Type,
                ProductId = realEstateAttribute.Product.Id,
                Title = realEstateAttribute.Product.Title,
                Views = realEstateAttribute.Product.Views,
                IsDisable = realEstateAttribute.Product.IsDisable,
                Price = realEstateAttribute.Product.Price,
                DateTime = realEstateAttribute.Product.DateTime,
                Categories = categories,
                Images = realEstateAttribute.Product.Images.Select(i => i.Image).ToList(),
                Area = realEstateAttribute.Area,
                Rooms = (int)realEstateAttribute.Rooms,
                IsRent = realEstateAttribute.IsRent
            };
            return result;
        }
    }
}
