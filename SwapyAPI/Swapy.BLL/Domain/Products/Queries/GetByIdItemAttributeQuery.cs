﻿using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetByIdItemAttributeQuery : IRequest<ItemAttribute>
    {
        public Guid ItemAttributeId { get; set; }
    }
}
