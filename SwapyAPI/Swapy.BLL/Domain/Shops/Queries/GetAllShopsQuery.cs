using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Shops.Queries
{
    public class GetAllShopsQuery : IRequest<IEnumerable<ShopAttribute>>
    {
        public string Title { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool SortByViews { get; set; }
        public bool ReverseSort { get; set; }
    }
}
