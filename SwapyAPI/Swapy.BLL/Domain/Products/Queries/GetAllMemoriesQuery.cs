using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllMemoriesQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
        public string ModelId { get; set; }
    }
}
