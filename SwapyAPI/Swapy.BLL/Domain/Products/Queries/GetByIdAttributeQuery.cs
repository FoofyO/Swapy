using MediatR;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetByIdAttributeQuery<T> : IRequest<T>
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public Language Language { get; set; }
    }
}
