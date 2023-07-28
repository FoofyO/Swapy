using MediatR;
using Swapy.Common.DTO.Categories.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetAllSubcategoriesByCategoryQuery : IRequest<IEnumerable<CategoryTreeResponseDTO>>
    {
        public string CategoryId { get; set; }
        public Language Language { get; set; }
    }
}
