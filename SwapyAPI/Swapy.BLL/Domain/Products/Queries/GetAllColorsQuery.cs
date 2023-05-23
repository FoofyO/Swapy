using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllColorsQuery : IRequest<IEnumerable<Color>>
    {
        public string ModelId { get; set; }
    }
}
