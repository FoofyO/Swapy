using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        private readonly ICityRepository _cityRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IShopAttributeRepository _shopRepository;
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;

        public NotificationService(UserManager<User> userManager, ICityRepository cityRepository, ICurrencyRepository currencyRepository, IShopAttributeRepository shopRepository, IEmailService emailService, IUserSubscriptionRepository userSubscriptionRepository)
        {
            _userManager = userManager;
            _emailService = emailService;
            _cityRepository = cityRepository;
            _shopRepository = shopRepository;
            _currencyRepository = currencyRepository;
            _userSubscriptionRepository = userSubscriptionRepository;
        }

        public async Task Notificate(NotificationModel model)
        {
            var sellerName = string.Empty;
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user.Type == UserType.Shop)
            {
                var shop = await _shopRepository.GetByUserIdAsync(model.UserId);
                sellerName = shop.ShopName;
            }
            else sellerName = user.FirstName + " " + user.LastName;

            var city = await _cityRepository.GetLocalizeByIdAsync(model.CityId, Language.English);
            var currency = await _currencyRepository.GetByIdAsync(model.CurrencyId);

            var subscribers = await _userSubscriptionRepository.GetUserSubscriptionsAsync(model.UserId);

            foreach (var subscriber in subscribers)
            {
                if (subscriber.IsSubscribed)
                {
                    await _emailService.SendNewProductNotificationAsync(subscriber.Email, sellerName, model.Title, model.Description, city, model.Price, currency.Name, model.ProductId);
                }
            }
        }
    }
}
