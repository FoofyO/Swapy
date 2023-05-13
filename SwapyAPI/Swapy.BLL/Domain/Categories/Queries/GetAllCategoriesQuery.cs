using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<Category>>
    {
    }
}
