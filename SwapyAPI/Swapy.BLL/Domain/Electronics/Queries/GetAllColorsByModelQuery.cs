using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Electronics.Queries
{
    public class GetAllColorsByModelQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public string ModelId { get; set; }
        public Language Language { get; set; }
    }
}
