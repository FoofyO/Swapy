﻿using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllModelsQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
        public List<string> ElectronicBrandsId { get; set; }
        public string ElectronicTypeId { get; set; }
    }
}
