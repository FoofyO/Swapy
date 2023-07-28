using MediatR;
using Swapy.Common.DTO.Categories.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetAllSubcategoriesBySubcategoryQuery : IRequest<IEnumerable<CategoryTreeResponseDTO>>
    {
        public string SubcategoryId { get; set; }
        public Language Language { get; set; }
    }
}
