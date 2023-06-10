using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Clothes.Queries
{
    public class GetAllGendersQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public Languages Language { get; set; }
    }
}
