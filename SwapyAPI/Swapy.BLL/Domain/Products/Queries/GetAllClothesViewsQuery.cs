using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllClothesViewsQuery : IRequest<IEnumerable<ClothesView>>
    {
        public string GenderId { get; set; }
        public string ClothesTypeId { get; set; }
    }
}
