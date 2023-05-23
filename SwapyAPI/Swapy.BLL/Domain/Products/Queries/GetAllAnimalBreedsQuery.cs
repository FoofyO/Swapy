using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllAnimalBreedsQuery : IRequest<IEnumerable<AnimalBreed>>
    {
        public string AnimalTypesId { get; set; }
    }
}
