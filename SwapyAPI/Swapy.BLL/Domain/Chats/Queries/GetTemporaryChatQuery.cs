using MediatR;
using Swapy.Common.DTO.Chats.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.BLL.Domain.Chats.Queries
{
    public class GetTemporaryChatQuery : IRequest<DetailChatResponseDTO>
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
    }
}
