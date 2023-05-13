using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetSubcategoriesBySubcategoryQuery : IRequest<IEnumerable<Subcategory>>
    {
        public string SubcategoryId { get; set; }
    }
}
