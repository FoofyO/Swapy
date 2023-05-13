using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Shops.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Shops.QueryHandlers
{
    public class GetAllShopsQueryHandler : IRequestHandler<GetAllShopsQuery, IEnumerable<ShopAttribute>>
    {
        private readonly IShopAttributeRepository _shopAttributeRepository;

        public GetAllShopsQueryHandler(IShopAttributeRepository shopAttributeRepository) => _shopAttributeRepository = shopAttributeRepository;

        public async Task<IEnumerable<ShopAttribute>> Handle(GetAllShopsQuery request, CancellationToken cancellationToken)
        {
            var query = await _shopAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x => request.Title == null || request.Title.Contains(x.ShopName));

            if (request.SortByViews == true) query.OrderBy(x => x.Views);
            else query.OrderBy(x => x.ShopName);
            if (request.ReverseSort == true) query.Reverse();
            
            var result = await query.ToListAsync();
            return result;
        }
    }
}
