﻿using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllClothesSizesQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
        public bool IsChild { get; set; }
        public bool IsShoe { get; set; }
    }
}
