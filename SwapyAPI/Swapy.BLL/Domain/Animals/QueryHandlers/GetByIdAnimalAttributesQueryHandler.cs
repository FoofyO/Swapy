using MediatR;
using Swapy.BLL.Domain.Animals.Queries;
using Swapy.Common.DTO.Animals.Responses;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Animals.QueryHandlers
{
    public class GetByIdAnimalAttributesQueryHandler : IRequestHandler<GetByIdAnimalAttributeQuery, AnimalAttributeResponseDTO>
    {
        private readonly IAnimalAttributeRepository _animalAttributeRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetByIdAnimalAttributesQueryHandler(IAnimalAttributeRepository animalAttributeRepository, ISubcategoryRepository subcategoryRepository)
        {
            _animalAttributeRepository = animalAttributeRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<AnimalAttributeResponseDTO> Handle(GetByIdAnimalAttributeQuery request, CancellationToken cancellationToken)
        {
            var animalAttribute = await _animalAttributeRepository.GetDetailByIdAsync(request.ProductId);
            List<CategoryNode> categories = (await _subcategoryRepository.GetSequenceOfSubcategories(animalAttribute.Product.SubcategoryId)).Select(s => new CategoryNode(s.Id, s.Name)).ToList();
            categories.Insert(0, new CategoryNode(animalAttribute.Product.CategoryId, animalAttribute.Product.Category.Name));

            AnimalAttributeResponseDTO result = new AnimalAttributeResponseDTO()
            {
                Id = animalAttribute.Id,
                City = animalAttribute.Product.City.Name,
                Currency = animalAttribute.Product.Currency.Name,
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
                DateTime = animalAttribute.Product.DateTime,
                Categories = categories,
                Images = animalAttribute.Product.Images.Select(i => i.Image).ToList(),
                BreedId = animalAttribute.AnimalBreedId,
                Breed = animalAttribute.AnimalBreed.Name
            };
            return result;
        }
    }
}
