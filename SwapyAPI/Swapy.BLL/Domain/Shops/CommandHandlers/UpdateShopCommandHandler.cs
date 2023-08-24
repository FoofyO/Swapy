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
            var shop = await _shopAttributeRepository.GetByUserIdAsync(request.UserId);

            if (!string.IsNullOrEmpty(request.Slogan)) shop.Slogan = request.Slogan;
            if (!string.IsNullOrEmpty(request.Location)) shop.Location = request.Location;
            if (!string.IsNullOrEmpty(request.ShopName)) shop.ShopName = request.ShopName;
            if (!string.IsNullOrEmpty(request.WorkDays)) shop.WorkDays = request.WorkDays;
            if (!string.IsNullOrEmpty(request.Description)) shop.Description = request.Description;
            if (request.EndWorkTime != null) shop.EndWorkTime = request.EndWorkTime;
            if (request.StartWorkTime != null) shop.StartWorkTime = request.StartWorkTime;

            await _shopAttributeRepository.UpdateAsync(shop);

            return Unit.Value;
        }
    }
}
