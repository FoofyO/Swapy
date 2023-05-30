using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllAutoBrandsQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
        public List<string> AutoTypesId { get; set; }
    }
}
