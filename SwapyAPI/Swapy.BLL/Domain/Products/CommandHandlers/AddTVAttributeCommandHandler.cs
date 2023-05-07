using MediatR;
using Swapy.BLL.CQRS.Commands;
using Swapy.BLL.Interfaces;
using Swapy.BLL.Services;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.CQRS.CommandHandlers
{
    public class AddTVAttributeCommandHandler : IRequestHandler<AddTVAttributeCommand, Unit>
    {
        private readonly Guid _userId;
        private readonly IProductRepository _productRepository;
        private readonly ITVAttributeRepository _tvAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public AddTVAttributeCommandHandler(Guid userId, IProductRepository productRepository, ITVAttributeRepository tvAttributeRepository, ISubcategoryRepository subcategoryRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
            _tvAttributeRepository = tvAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<Unit> Handle(AddTVAttributeCommand request, CancellationToken cancellationToken)
        {
            ISubcategoryService subcategoryService = new SubcategoryService(_subcategoryRepository);
            if (!await subcategoryService.SubcategoryValidationAsync(request.SubcategoryId)) throw new ArgumentException("Invalid subcategory.");

            Product product = new Product(request.Title, request.Description, request.Price, _userId, request.CurrencyId, request.CategoryId, request.SubcategoryId, request.CityId);
            await _productRepository.CreateAsync(product);

            TVAttribute tvAttribute = new TVAttribute(request.IsNew, request.IsSmart, request.TVTypeId, request.TVBrandId, request.ScreenResolutionId, request.ScreenDiagonalId, product.Id);
            await _tvAttributeRepository.CreateAsync(tvAttribute);

            return Unit.Value;
        }
    }
}
