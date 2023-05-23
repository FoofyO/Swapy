﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllColorsQueryHandler : IRequestHandler<GetAllColorsQuery, IEnumerable<Color>>
    {
        private readonly IColorRepository _colorRepository;

        public GetAllColorsQueryHandler(IColorRepository colorRepository) => _colorRepository = colorRepository;

        public async Task<IEnumerable<Color>> Handle(GetAllColorsQuery request, CancellationToken cancellationToken)
        { 
            var result = await _colorRepository.GetByModelAsync(request.ModelId);
            return result;
        }
    }
}
