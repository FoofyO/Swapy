using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetByIdFavoriteProductQueryHandler : IRequestHandler<GetByIdFavoriteProductQuery, FavoriteProduct>
    {
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public GetByIdFavoriteProductQueryHandler(IFavoriteProductRepository favoriteProductRepository)
        {
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<FavoriteProduct> Handle(GetByIdFavoriteProductQuery request, CancellationToken cancellationToken)
        {
            return await _favoriteProductRepository.GetDetailByIdAsync(request.FavoriteProductId);
        }
    }
}
