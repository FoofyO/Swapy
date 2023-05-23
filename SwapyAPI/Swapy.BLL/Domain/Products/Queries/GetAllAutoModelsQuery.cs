using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllAutoModelsQuery : IRequest<IEnumerable<AutoModel>>
    {
        public List<string> AutoBrandsId { get; set; }
        public List<string> AutoTypesId { get; set; }
    }
}
