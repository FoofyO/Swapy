using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Items.Queries
{
    public class GetAllItemSectionsQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public Language Language { get; set; }
    }
}
