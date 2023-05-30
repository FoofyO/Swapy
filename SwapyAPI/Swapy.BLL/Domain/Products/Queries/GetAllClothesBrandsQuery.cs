using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllClothesBrandsQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
        public List<string> ClothesViewsId { get; set; }
    }
}
