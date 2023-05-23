using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllClothesBrandsQuery : IRequest<IEnumerable<ClothesBrand>>
    {
        public List<string> ClothesViewsId { get; set; }
    }
}
