using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetAllSubcategoriesBySubcategoryQuery : IRequest<IEnumerable<Subcategory>>
    {
        public string SubcategoryId { get; set; }
    }
}
