using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Clothes.Queries
{
    public class GetAllClothesSeasonsQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public Language Language { get; set; }
    }
}
