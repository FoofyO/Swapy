using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Clothes.Queries
{
    public class GetAllClothesTypesQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
        public string GenderId { get; set; }
    }
}
