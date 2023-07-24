using MediatR;
using Swapy.BLL.Domain.Items.Queries;
using Swapy.Common.DTO.Items.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Items.QueryHandlers
{
    public class GetByIdItemAttributeQueryHandler : IRequestHandler<GetByIdItemAttributeQuery, ItemAttributeResponseDTO>
    {
        private readonly IItemAttributeRepository _itemAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetByIdItemAttributeQueryHandler(IItemAttributeRepository itemAttributeRepository, ISubcategoryRepository subcategoryRepository)
        {
            _itemAttributeRepository = itemAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<ItemAttributeResponseDTO> Handle(GetByIdItemAttributeQuery request, CancellationToken cancellationToken)
        {
            var itemAttribute = await _itemAttributeRepository.GetDetailByIdAsync(request.ProductId);
            List<CategoryNode> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(itemAttribute.Product.SubcategoryId, request.Language)).ToList();
            categories.Insert(0, new CategoryNode(itemAttribute.Product.CategoryId, itemAttribute.Product.Category.Names.FirstOrDefault(l => l.Language == request.Language).Value));

            ItemAttributeResponseDTO result = new ItemAttributeResponseDTO()
            {
                Id = itemAttribute.Id,
                City = itemAttribute.Product.City.Names.FirstOrDefault(l => l.Language == request.Language).Value,
                Currency = itemAttribute.Product.Currency.Name,
                CurrencySymbol = itemAttribute.Product.Currency.Symbol,
                UserId = itemAttribute.Product.UserId,
                FirstName = itemAttribute.Product.User.FirstName,
                LastName = itemAttribute.Product.User.LastName,
                PhoneNumber = itemAttribute.Product.User.PhoneNumber,
                ShopId = itemAttribute.Product.User.ShopAttributeId ?? string.Empty,
                Shop = itemAttribute.Product.User.ShopAttribute?.ShopName ?? string.Empty,
                UserType = itemAttribute.Product.User.Type,
                ProductId = itemAttribute.Product.Id,
                IsDisable = itemAttribute.Product.IsDisable,
                Title = itemAttribute.Product.Title,
                Views = itemAttribute.Product.Views,
                Price = itemAttribute.Product.Price,
                DateTime = itemAttribute.Product.DateTime,
                Categories = categories,
                Images = itemAttribute.Product.Images.Select(i => i.Image).ToList(),
                IsNew = itemAttribute.IsNew
            };
            return result;
        }
    }
}
