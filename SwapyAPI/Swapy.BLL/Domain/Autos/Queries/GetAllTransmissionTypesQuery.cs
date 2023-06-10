using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Autos.Queries
{
    public class GetAllTransmissionTypesQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public Languages Language { get; set; }
    }
}
