using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Animals.Queries
{
    public class GetAllAnimalTypesQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public Language Language { get; set; }
    }
}
