using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Clothes.Queries
{
    public class GetAllClothesViewsQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
        public string GenderId { get; set; }
        public string ClothesTypeId { get; set; }
    }
}
