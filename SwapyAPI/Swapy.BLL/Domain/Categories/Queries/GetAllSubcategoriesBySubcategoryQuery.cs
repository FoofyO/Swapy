using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetAllSubcategoriesBySubcategoryQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public string SubcategoryId { get; set; }
        public Languages Language { get; set; }
    }
}
