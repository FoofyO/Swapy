﻿using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllModelsQuery : IRequest<IEnumerable<Model>>
    {
        public string ElectronicBrandId { get; set; }
        public string ElectronicTypeId { get; set; }
    }
}
