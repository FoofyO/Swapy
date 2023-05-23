using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllItemTypesQuery : IRequest<IEnumerable<Subcategory>>
    {
    }
}
