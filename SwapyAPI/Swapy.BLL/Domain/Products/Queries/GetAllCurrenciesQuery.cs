using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllCurrenciesQuery : IRequest<IEnumerable<Currency>>
    {
    }
}
