﻿using MediatR;
using Swapy.BLL.Domain.RealEstates.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.RealEstates.QueryHandlers
{
    public class GetAllRealEstateAttributesQueryHandler : IRequestHandler<GetAllRealEstateAttributesQuery, ProductsResponseDTO<ProductResponseDTO>>
    {
        private readonly IRealEstateAttributeRepository _realEstateAttributeRepository;

        public GetAllRealEstateAttributesQueryHandler(IRealEstateAttributeRepository animalAttributeRepository, IFavoriteProductRepository favoriteProductRepository) => _realEstateAttributeRepository = animalAttributeRepository;

        public async Task<ProductsResponseDTO<ProductResponseDTO>> Handle(GetAllRealEstateAttributesQuery request, CancellationToken cancellationToken)
        {
            return await _realEstateAttributeRepository.GetAllFilteredAsync(request.Page,
                                                                            request.PageSize,
                                                                            request.UserId,
                                                                            request.Title,
                                                                            request.CurrencyId,
                                                                            request.PriceMin,
                                                                            request.PriceMax,
                                                                            request.CategoryId,
                                                                            request.SubcategoryId,
                                                                            request.CityId,
                                                                            request.OtherUserId,
                                                                            request.IsRent,
                                                                            request.AreaMax,
                                                                            request.AreaMin,
                                                                            request.RoomsMin,
                                                                            request.RoomsMax,
                                                                            request.RealEstateTypesId,
                                                                            request.SortByPrice,
                                                                            request.ReverseSort,
                                                                            request.Language);
        }
    }
}
