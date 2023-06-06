using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Items.Queries
{
    public class GetAllItemTypesQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
        public string ParentSubcategoryId { get; set; }
    }
}
