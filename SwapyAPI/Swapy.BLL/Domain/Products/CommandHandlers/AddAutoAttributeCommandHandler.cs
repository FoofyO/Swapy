using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.BLL.Interfaces;
using Swapy.BLL.Services;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class AddAutoAttributeCommandHandler : IRequestHandler<AddAutoAttributeCommand, AutoAttribute>
    {
        private readonly string _userId;
        private readonly IProductRepository _productRepository;
        private readonly IAutoAttributeRepository _autoAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public AddAutoAttributeCommandHandler(IProductRepository productRepository, IAutoAttributeRepository autoAttributeRepository, ISubcategoryRepository subcategoryRepository)
        {
            _productRepository = productRepository;
            _autoAttributeRepository = autoAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<AutoAttribute> Handle(AddAutoAttributeCommand request, CancellationToken cancellationToken)
        {
            ISubcategoryService subcategoryService = new SubcategoryService(_subcategoryRepository);
            if (!await subcategoryService.SubcategoryValidationAsync(request.SubcategoryId)) throw new ArgumentException("Invalid subcategory");

            Product product = new Product(request.Title, request.Description, request.Price, _userId, request.CurrencyId, request.CategoryId, request.SubcategoryId, request.CityId);
            await _productRepository.CreateAsync(product);

            AutoAttribute autoAttribute = new AutoAttribute(request.Miliage, request.EngineCapacity, request.ReleaseYear, request.IsNew, request.FuelTypeId, request.AutoColorId, request.TransmissionTypeId, request.AutoBrandTypeId, product.Id);
            await _autoAttributeRepository.CreateAsync(autoAttribute);

            return autoAttribute;
        }
    }
}
