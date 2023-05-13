using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetAllSubcategoriesByCategoryQuery : IRequest<IEnumerable<Subcategory>>
    {
        public string CategoryId { get; set; }
    }
}
