using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Autos.Queries
{
    public class GetAllFuelTypesQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public Language Language { get; set; }
    }
}
