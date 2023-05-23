﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllClothesSizesQueryHandler : IRequestHandler<GetAllClothesSizesQuery, IEnumerable<ClothesSize>>
    {
        private readonly IClothesSizeRepository _clothesSizeRepository;

        public GetAllClothesSizesQueryHandler(IClothesSizeRepository clothesSizeRepository) => _clothesSizeRepository = clothesSizeRepository;

        public async Task<IEnumerable<ClothesSize>> Handle(GetAllClothesSizesQuery request, CancellationToken cancellationToken)
        {
            var result = await _clothesSizeRepository.GetAllAsync();
            return result;
        }
    }
}
