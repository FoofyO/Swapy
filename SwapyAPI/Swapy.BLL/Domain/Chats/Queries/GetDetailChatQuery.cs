﻿using MediatR;
using Swapy.Common.DTO.Chats.Responses;

namespace Swapy.BLL.Domain.Chats.Queries
{
    public class GetDetailChatQuery : IRequest<DetailChatResponseDTO>
    {
        public string ChatId { get; set; }
    }
}
