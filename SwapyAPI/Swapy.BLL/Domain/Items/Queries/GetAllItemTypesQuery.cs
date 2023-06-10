using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Items.Queries
{
    public class GetAllItemTypesQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public string ParentSubcategoryId { get; set; }
        public Languages Language { get; set;}
    }
}
