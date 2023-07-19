using MediatR;
using Swapy.BLL.Domain.TVs.Commands;
using Swapy.BLL.Interfaces;
using Swapy.BLL.Services;
using Swapy.Common.Entities;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.TVs.CommandHandlers
{
    public class AddTVAttributeCommandHandler : IRequestHandler<AddTVAttributeCommand, TVAttribute>
    {
        private readonly IProductRepository _productRepository;
        private readonly INotificationService _notificationService;
        private readonly ITVAttributeRepository _tvAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public AddTVAttributeCommandHandler(IProductRepository productRepository, ITVAttributeRepository tvAttributeRepository, ISubcategoryRepository subcategoryRepository, INotificationService notificationService)
        {
            _productRepository = productRepository;
            _notificationService = notificationService;
            _tvAttributeRepository = tvAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<TVAttribute> Handle(AddTVAttributeCommand request, CancellationToken cancellationToken)
        {
            ISubcategoryService subcategoryService = new SubcategoryService(_subcategoryRepository);
            if (!await subcategoryService.SubcategoryValidationAsync(request.SubcategoryId)) throw new ArgumentException("Invalid subcategory");

            Product product = new Product(request.Title, request.Description, request.Price, request.UserId, request.CurrencyId, request.CategoryId, request.SubcategoryId, request.CityId);
            await _productRepository.CreateAsync(product);

            TVAttribute tvAttribute = new TVAttribute(request.IsNew, request.IsSmart, request.TVTypeId, request.TVBrandId, request.ScreenResolutionId, request.ScreenDiagonalId, product.Id);
            await _tvAttributeRepository.CreateAsync(tvAttribute);

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

            return tvAttribute;
        }
    }
}
