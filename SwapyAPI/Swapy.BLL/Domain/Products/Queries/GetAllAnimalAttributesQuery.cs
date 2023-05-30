using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllAnimalAttributesQuery : GetAllProductQuery<ProductResponseDTO>
    {
        public List<string> AnimalBreedsId { get; set; }
        public List<string> AnimalTypesId { get; set; }
    }
}
