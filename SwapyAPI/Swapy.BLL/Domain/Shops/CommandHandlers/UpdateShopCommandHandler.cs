using MediatR;
using Swapy.BLL.Domain.Shops.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.Shops.CommandHandlers
{
    public class UpdateShopCommandHandler : IRequestHandler<UpdateShopCommand, Unit>
    {
        private readonly string _userId;
        private readonly IShopAttributeRepository _shopAttributeRepository;

        public UpdateShopCommandHandler(IShopAttributeRepository shopAttributeRepository) => _shopAttributeRepository = shopAttributeRepository;

        public async Task<Unit> Handle(UpdateShopCommand request, CancellationToken cancellationToken)
        {
            var shop = await _shopAttributeRepository.GetByIdAsync(request.ShopId);

            if (!shop.UserId.Equals(_userId)) throw new NoAccessException("No access to update this shop");
            
            shop.Banner = request.Banner;
            shop.Slogan = request.Slogan;
            shop.Location = request.Location;
            shop.ShopName = request.ShopName;
            shop.WorkDays = request.WorkDays;
            shop.Description = request.Description;
            shop.EndWorkTime = request.EndWorkTime;
            shop.StartWorkTime = request.StartWorkTime;

            await _shopAttributeRepository.UpdateAsync(shop);

            return Unit.Value;
        }
    }
}
