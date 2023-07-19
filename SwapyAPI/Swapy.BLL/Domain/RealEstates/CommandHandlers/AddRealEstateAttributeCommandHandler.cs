using MediatR;
using Swapy.BLL.Domain.RealEstates.Commands;
using Swapy.BLL.Interfaces;
using Swapy.BLL.Services;
using Swapy.Common.Entities;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;


namespace Swapy.BLL.Domain.RealEstates.CommandHandlers
{
    public class AddRealEstateAttributeCommandHandler : IRequestHandler<AddRealEstateAttributeCommand, RealEstateAttribute>
    {
        private readonly IProductRepository _productRepository;
        private readonly INotificationService _notificationService;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IRealEstateAttributeRepository _realEstateAttributeRepository;

        public AddRealEstateAttributeCommandHandler(IProductRepository productRepository, IRealEstateAttributeRepository realEstateAttributeRepository, ISubcategoryRepository subcategoryRepository, INotificationService notificationService)
        {
            _productRepository = productRepository;
            _notificationService = notificationService;
            _subcategoryRepository = subcategoryRepository;
            _realEstateAttributeRepository = realEstateAttributeRepository;
        }

        public async Task<RealEstateAttribute> Handle(AddRealEstateAttributeCommand request, CancellationToken cancellationToken)
        {
            ISubcategoryService subcategoryService = new SubcategoryService(_subcategoryRepository);
            if (!await subcategoryService.SubcategoryValidationAsync(request.SubcategoryId)) throw new ArgumentException("Invalid subcategory");

            Product product = new Product(request.Title, request.Description, request.Price, request.UserId, request.CurrencyId, request.CategoryId, request.SubcategoryId, request.CityId);
            await _productRepository.CreateAsync(product);

            RealEstateAttribute animalAttribute = new RealEstateAttribute(request.Area, request.Rooms, request.IsRent, request.RealEstateTypeId, product.Id);
            await _realEstateAttributeRepository.CreateAsync(animalAttribute);

            var model = new NotificationModel()
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                CityId = request.CityId,
                Price = request.Price,
                CurrencyId = request.CurrencyId,
                ProductId = product.Id
            };

            await _notificationService.Notificate(model);

            return animalAttribute;
        }
    }
}
