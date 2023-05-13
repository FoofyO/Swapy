using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class RemoveFavoriteProductCommandHandler : IRequestHandler<RemoveFavoriteProductCommand, Unit>
    {
        private readonly string _userId;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public RemoveFavoriteProductCommandHandler(string userId, IFavoriteProductRepository favoriteProductRepository)
        {
            _userId = userId;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<Unit> Handle(RemoveFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            var favoriteProduct = await _favoriteProductRepository.GetByIdAsync(request.FavoriteProductId);

            if (_userId != favoriteProduct.UserId) throw new NoAccessException("No access to delete this product.");

            await _favoriteProductRepository.DeleteAsync(favoriteProduct);

            return Unit.Value;
        }
    }
}
