using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetAllSubcategoriesByCategoryQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
        public string CategoryId { get; set; }
    }
}
