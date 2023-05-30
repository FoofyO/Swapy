using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllScreenDiagonalsQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
    }
}
