using MediatR;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetByIdAttributeQuery<T> : IRequest<T>
    {
        public string ProductId { get; set; }
    }
}
