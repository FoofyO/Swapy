using MediatR;
using Swapy.Common.DTO.Categories.Responses;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryTreeResponseDTO>>
    {
        public Language Language { get; set; }
    }
}
