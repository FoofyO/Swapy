using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllCitiesQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public Languages Language { get; set; }
    }
}
