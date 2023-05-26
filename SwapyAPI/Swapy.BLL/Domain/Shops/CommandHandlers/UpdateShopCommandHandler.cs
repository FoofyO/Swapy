using MediatR;
using Swapy.BLL.Domain.Shops.Commands;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Shops.CommandHandlers
{
    public class UpdateShopCommandHandler : IRequestHandler<UpdateShopCommand, Unit>
    {
        private readonly IShopAttributeRepository _shopAttributeRepository;

        public UpdateShopCommandHandler(IShopAttributeRepository shopAttributeRepository) => _shopAttributeRepository = shopAttributeRepository;

        public async Task<Unit> Handle(UpdateShopCommand request, CancellationToken cancellationToken)
        {
            var shop = await _shopAttributeRepository.GetByUserId(request.UserId);

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
