using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, Unit>
    {
        private readonly IImageService _imageService;
        private readonly IChatRepository _chatRepository;
        private readonly IAnimalAttributeRepository _animalAttributeRepository;
        private readonly IAutoAttributeRepository _autoAttributeRepository;
        private readonly IClothesAttributeRepository _clothesAttributeRepository;
        private readonly IElectronicAttributeRepository _electronicAttributeRepository;
        private readonly IItemAttributeRepository _itemAttributeRepository;
        private readonly IRealEstateAttributeRepository _realEstateAttributeRepository;
        private readonly ITVAttributeRepository _tvAttributeRepository;
        private readonly IProductRepository _productRepository;

        public RemoveProductCommandHandler(IImageService imageService, IChatRepository chatRepository, IAnimalAttributeRepository animalAttributeRepository, IAutoAttributeRepository autoAttributeRepository, IClothesAttributeRepository clothesAttributeRepository, IElectronicAttributeRepository electronicAttributeRepository, IItemAttributeRepository itemAttributeRepository, IRealEstateAttributeRepository realEstateAttributeRepository, ITVAttributeRepository tvAttributeRepository, IProductRepository productRepository)
        {
            _imageService = imageService;
            _chatRepository = chatRepository;
            _animalAttributeRepository = animalAttributeRepository;
            _autoAttributeRepository = autoAttributeRepository;
            _clothesAttributeRepository = clothesAttributeRepository;
            _electronicAttributeRepository = electronicAttributeRepository;
            _itemAttributeRepository = itemAttributeRepository;
            _realEstateAttributeRepository = realEstateAttributeRepository;
            _tvAttributeRepository = tvAttributeRepository;
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if (!request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to delete this product");

            await _imageService.RemoveAllProductImagesAsync(product.Id);

            await _chatRepository.DeleteChatsByProductId(product.Id);

            if(product.AnimalAttributeId != null) { await _animalAttributeRepository.DeleteByIdAsync(product.AnimalAttributeId); }
            else if (product.AutoAttributeId != null) { await _autoAttributeRepository.DeleteByIdAsync(product.AutoAttributeId); }
            else if (product.ClothesAttributeId != null) { await _animalAttributeRepository.DeleteByIdAsync(product.ClothesAttributeId); }
            else if (product.ElectronicAttributeId != null) { await _electronicAttributeRepository.DeleteByIdAsync(product.ElectronicAttributeId); }
            else if (product.ItemAttributeId != null) { await _itemAttributeRepository.DeleteByIdAsync(product.ItemAttributeId); }
            else if (product.RealEstateAttributeId != null) { await _realEstateAttributeRepository.DeleteByIdAsync(product.RealEstateAttributeId); }
            else if (product.TVAttributeId != null) { await _tvAttributeRepository.DeleteByIdAsync(product.TVAttributeId); }

            await _productRepository.DeleteAsync(product);

            return Unit.Value;
        }
    }
}
