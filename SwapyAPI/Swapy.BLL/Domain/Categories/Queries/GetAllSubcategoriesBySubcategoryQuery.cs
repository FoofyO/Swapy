using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetAllSubcategoriesBySubcategoryQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
        public string SubcategoryId { get; set; }
    }
}
