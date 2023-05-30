using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetByIdElectronicAttributeQueryHandler : IRequestHandler<GetByIdElectronicAttributeQuery, ElectronicAttributeResponseDTO>
    {
        private readonly IElectronicAttributeRepository _electronicAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetByIdElectronicAttributeQueryHandler(IElectronicAttributeRepository electronicAttributeRepository, ISubcategoryRepository subcategoryRepository)
        {
            _electronicAttributeRepository = electronicAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<ElectronicAttributeResponseDTO> Handle(GetByIdElectronicAttributeQuery request, CancellationToken cancellationToken)
        {
            var electronicAttribute = await _electronicAttributeRepository.GetDetailByIdAsync(request.ProductId);
            List<CategoryNode> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(electronicAttribute.Product.SubcategoryId)).Select(s => new CategoryNode(s.Id, s.Name)).ToList();
            categories.Insert(0, new CategoryNode(electronicAttribute.Product.CategoryId, electronicAttribute.Product.Category.Name));

            ElectronicAttributeResponseDTO result = new ElectronicAttributeResponseDTO()
            {
                Id = electronicAttribute.Id,
                City = electronicAttribute.Product.City.Name,
                Currency = electronicAttribute.Product.Currency.Name,
                CurrencySymbol = electronicAttribute.Product.Currency.Symbol,
                UserId = electronicAttribute.Product.UserId,
                FirstName = electronicAttribute.Product.User.FirstName,
                LastName = electronicAttribute.Product.User.LastName,
                PhoneNumber = electronicAttribute.Product.User.PhoneNumber,
                ShopId = electronicAttribute.Product.User.ShopAttributeId,
                Shop = electronicAttribute.Product.User.ShopAttribute.ShopName,
                UserType = electronicAttribute.Product.User.Type,
                ProductId = electronicAttribute.Product.Id,
                Title = electronicAttribute.Product.Title,
                Views = electronicAttribute.Product.Views,
                Price = electronicAttribute.Product.Price,
                DateTime = electronicAttribute.Product.DateTime,
                Categories = categories,
                Images = electronicAttribute.Product.Images.Select(i => i.Image).ToList(),
                IsNew = electronicAttribute.IsNew,
                ColorId = electronicAttribute.ModelColor.ColorId,
                Color = electronicAttribute.ModelColor.Color.Name,
                MemoryId = electronicAttribute.MemoryModel.MemoryId,
                Memory = electronicAttribute.MemoryModel.Memory.Name,
                ModelId = electronicAttribute.ModelColor.ModelId,
                Model = electronicAttribute.ModelColor.Model.Name,
                ElectronicBrandId = electronicAttribute.ModelColor.Model.ElectronicBrandType.ElectronicBrandId,
                ElectronicBrand = electronicAttribute.ModelColor.Model.ElectronicBrandType.ElectronicBrand.Name
            };
            return result;
        }
    }
}
