﻿using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllAutoTypesQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
    }
}
