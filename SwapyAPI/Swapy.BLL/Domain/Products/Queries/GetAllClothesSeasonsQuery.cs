using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllClothesSeasonsQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
    }
}
