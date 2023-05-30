using MediatR;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Services
{
    public class FavoriteProductsService
    {
        private IFavoriteProductRepository _favoriteProductRepository { get; set; }

        public FavoriteProductsService(IFavoriteProductRepository favoriteProductRepository)
        {
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<bool> IsFavoriteAsync(string productId, string userId)
        {
            try
            {
                await _favoriteProductRepository.GetByProductAndUserIdAsync(productId, userId);
            }
            catch (NotFoundException)
            {
                return false;
            }
            return true;
        }
    }
}
