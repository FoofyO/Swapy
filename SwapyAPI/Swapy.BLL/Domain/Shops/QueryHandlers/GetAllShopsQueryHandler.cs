using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Shops.Queries;
using Swapy.Common.DTO.Shops.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Shops.QueryHandlers
{
    public class GetAllShopsQueryHandler : IRequestHandler<GetAllShopsQuery, ShopsResponseDTO>
    {
        private readonly IShopAttributeRepository _shopAttributeRepository;

        public GetAllShopsQueryHandler(IShopAttributeRepository shopAttributeRepository) => _shopAttributeRepository = shopAttributeRepository;

        public async Task<ShopsResponseDTO> Handle(GetAllShopsQuery request, CancellationToken cancellationToken)
        {
            var query = await _shopAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x => request.Title == null || x.ShopName.Contains(request.Title));

            if (request.SortByViews == true) query.OrderBy(x => x.Views);
            else query.OrderBy(x => x.ShopName);
            if (request.ReverseSort == true) query.Reverse();

            var result = await query.Select(x => new ShopResponseDTO
            {
                ShopId = x.Id,
                Logo = x.User.Logo,
                Description = x.Description,
                PhoneNumber = x.User.PhoneNumber,
                ProductCount = x.User.ProductsCount
            }).ToListAsync();

            return new ShopsResponseDTO(result, query.Count(), (int)Math.Ceiling(Convert.ToDouble(query.Count() / request.PageSize)));
        }
    }
}
