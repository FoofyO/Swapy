using MediatR;
using Swapy.Common.DTO.Categories.Responses;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetSubcategoryPathQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public string SubcategoryId { get; set; }
        public Language Language { get; set; }
    }
}
