using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllMemoriesQuery : IRequest<IEnumerable<Memory>>
    {
    }
}
