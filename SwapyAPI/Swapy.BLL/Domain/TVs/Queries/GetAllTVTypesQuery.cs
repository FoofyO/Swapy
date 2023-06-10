using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.TVs.Queries
{
    public class GetAllTVTypesQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public Languages Language { get; set; }
    }
}
