using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public abstract class AddProductCommand : IRequest<Unit>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubcategoryId { get; set; }
        public Guid CityId { get; set; }
    }
}
