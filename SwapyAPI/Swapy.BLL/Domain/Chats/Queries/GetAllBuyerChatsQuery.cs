using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Chats.Queries
{
    public class GetAllBuyerChatsQuery : IRequest<List<Chat>>
    {

    } 
}