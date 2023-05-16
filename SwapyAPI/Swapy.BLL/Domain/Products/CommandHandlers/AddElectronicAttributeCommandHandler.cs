using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.BLL.Interfaces;
using Swapy.BLL.Services;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class AddElectronicAttributeCommandHandler : IRequestHandler<AddElectronicAttributeCommand, ElectronicAttribute>
    {
        private readonly string _userId;
        private readonly IProductRepository _productRepository;
        private readonly IElectronicAttributeRepository _electronicAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public AddElectronicAttributeCommandHandler(string userId, IProductRepository productRepository, IElectronicAttributeRepository electronicAttributeRepository, ISubcategoryRepository subcategoryRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _electronicAttributeRepository = electronicAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<ElectronicAttribute> Handle(AddElectronicAttributeCommand request, CancellationToken cancellationToken)
        {
            ISubcategoryService subcategoryService = new SubcategoryService(_subcategoryRepository);
            if (!await subcategoryService.SubcategoryValidationAsync(request.SubcategoryId)) throw new ArgumentException("Invalid subcategory");

            Product product = new Product(request.Title, request.Description, request.Price, _userId, request.CurrencyId, request.CategoryId, request.SubcategoryId, request.CityId);
            await _productRepository.CreateAsync(product);

            ElectronicAttribute electronicAttribute = new ElectronicAttribute(request.IsNew, request.MemoryModelId, request.ModelColorId, product.Id);
            await _electronicAttributeRepository.CreateAsync(electronicAttribute);

            return electronicAttribute;
        }
    }
}
