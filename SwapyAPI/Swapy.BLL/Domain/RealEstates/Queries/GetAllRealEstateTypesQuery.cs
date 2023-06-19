using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.RealEstates.Queries
{
    public class GetAllRealEstateTypesQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public Language Language { get; set; }
    }
}
