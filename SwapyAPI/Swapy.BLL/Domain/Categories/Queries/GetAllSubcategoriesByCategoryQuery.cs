using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetAllSubcategoriesByCategoryQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public string CategoryId { get; set; }
        public Language Language { get; set; }
    }
}
