using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
    }
}
