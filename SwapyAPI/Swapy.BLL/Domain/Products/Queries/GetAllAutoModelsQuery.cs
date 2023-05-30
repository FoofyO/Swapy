using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllAutoModelsQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
        public List<string> AutoBrandsId { get; set; }
        public List<string> AutoTypesId { get; set; }
    }
}
