using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public Languages Language { get; set; }
    }
}
