﻿using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class SwitchProductEnablingCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
    }
}
