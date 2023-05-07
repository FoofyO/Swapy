using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetByIdClothesAttributeQuery : IRequest<ClothesAttribute>
    {
        public Guid ClothesAttributeId { get; set; }
    }
}
