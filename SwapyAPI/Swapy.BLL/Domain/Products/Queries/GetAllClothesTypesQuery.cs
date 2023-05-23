using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllClothesTypesQuery : IRequest<IEnumerable<Subcategory>>
    {
        public string GenderId { get; set; }
    }
}
