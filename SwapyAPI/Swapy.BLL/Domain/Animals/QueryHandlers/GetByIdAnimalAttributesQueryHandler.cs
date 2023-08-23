using MediatR;
using Swapy.BLL.Domain.Animals.Queries;
using Swapy.Common.DTO.Animals.Responses;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;
using System.Runtime.InteropServices;

namespace Swapy.BLL.Domain.Animals.QueryHandlers
{
    public class GetByIdAnimalAttributesQueryHandler : IRequestHandler<GetByIdAnimalAttributeQuery, AnimalAttributeResponseDTO>
    {
        private readonly IAnimalAttributeRepository _animalAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public GetByIdAnimalAttributesQueryHandler(IAnimalAttributeRepository animalAttributeRepository, ISubcategoryRepository subcategoryRepository, IFavoriteProductRepository favoriteProductRepository)
        {
            _animalAttributeRepository = animalAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<AnimalAttributeResponseDTO> Handle(GetByIdAnimalAttributeQuery request, CancellationToken cancellationToken)
        {
            var animalAttribute = await _animalAttributeRepository.GetDetailByIdAsync(request.ProductId);
            List<SpecificationResponseDTO<string>> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(animalAttribute.Product.SubcategoryId, request.Language)).ToList();
            categories.Insert(0, new SpecificationResponseDTO<string>(animalAttribute.Product.CategoryId, animalAttribute.Product.Category.Names.FirstOrDefault(l => l.Language == request.Language).Value));

            AnimalAttributeResponseDTO result = new AnimalAttributeResponseDTO()
            {
                Id = animalAttribute.Id,
                City = animalAttribute.Product.City.Names.FirstOrDefault(l => l.Language == request.Language).Value,
                CityId = animalAttribute.Product.City.Id,
                Currency = animalAttribute.Product.Currency.Name,
                CurrencyId = animalAttribute.Product.Currency.Id,
                CurrencySymbol = animalAttribute.Product.Currency.Symbol,
                UserId = animalAttribute.Product.UserId,
                FirstName = animalAttribute.Product.User.FirstName,
                LastName = animalAttribute.Product.User.LastName,
                PhoneNumber = animalAttribute.Product.User.PhoneNumber,
                ShopId = animalAttribute.Product.User.ShopAttributeId,
                Shop = animalAttribute.Product.User.ShopAttribute.ShopName,
                UserType = animalAttribute.Product.User.Type,
                ProductId = animalAttribute.Product.Id,
                Title = animalAttribute.Product.Title,
                Views = animalAttribute.Product.Views,
                Price = animalAttribute.Product.Price,
                Description = animalAttribute.Product.Description,
                DateTime = animalAttribute.Product.DateTime,
                IsFavorite = await _favoriteProductRepository.CheckProductOnFavorite(animalAttribute.Product.Id, request.UserId),
                Categories = categories,
                IsDisable = animalAttribute.Product.IsDisable,
                Images = animalAttribute.Product.Images.Select(i => i.Image).ToList(),
                BreedId = animalAttribute.AnimalBreedId,
                Breed = animalAttribute.AnimalBreed.Names.FirstOrDefault(l => l.Language == request.Language).Value
            };
            return result;
        }
    }
}
