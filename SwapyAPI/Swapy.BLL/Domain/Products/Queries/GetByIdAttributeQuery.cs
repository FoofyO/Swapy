using MediatR;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetByIdAttributeQuery<T> : IRequest<T>
    {
        public string ProductId { get; set; }
        public Languages Language { get; set; }
    }
}
