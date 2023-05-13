using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetSubcategoriesByCategoryQuery : IRequest<IEnumerable<Subcategory>>
    {
        public string CategoryId { get; set; }
    }
}
