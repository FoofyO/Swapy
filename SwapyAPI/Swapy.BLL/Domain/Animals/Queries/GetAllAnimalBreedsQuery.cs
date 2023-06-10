using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Animals.Queries
{
    public class GetAllAnimalBreedsQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public string AnimalTypesId { get; set; }
        public Languages Language { get; set; }
    }
}
