using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;


namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class AddFavoriteProductCommandHandler : IRequestHandler<AddFavoriteProductCommand, FavoriteProduct>
    {
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public AddFavoriteProductCommandHandler(IFavoriteProductRepository favoriteProductRepository)
        {
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<FavoriteProduct> Handle(AddFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            FavoriteProduct favoriteProduct = new FavoriteProduct(request.UserId, request.ProductId);
            await _favoriteProductRepository.CreateAsync(favoriteProduct);

            return favoriteProduct;
        }
    }
}
