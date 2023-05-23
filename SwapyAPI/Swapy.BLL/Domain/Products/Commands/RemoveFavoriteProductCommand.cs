﻿using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveFavoriteProductCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string FavoriteProductId { get; set; }
    }
}
