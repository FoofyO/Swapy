using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Animals.Queries
{
    public class GetAllAnimalBreedsQuery : IRequest<IEnumerable<SpecificationResponseDTO>>
    {
        public string AnimalTypesId { get; set; }
    }
}
