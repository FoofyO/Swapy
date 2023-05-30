using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllClothesTypesQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
        public string GenderId { get; set; }
    }
}
