using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Clothes.Queries
{
    public class GetAllClothesViewsQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public string GenderId { get; set; }
        public string ClothesTypeId { get; set; }
        public Languages Language { get; set; }
    }
}
