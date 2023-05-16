using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllTVBrandsQuery : IRequest<IEnumerable<TVBrand>>
    {
    }
}
