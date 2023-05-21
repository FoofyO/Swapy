using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.BLL.Interfaces;
using Swapy.BLL.Services;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class AddFavoriteProductCommandHandler : IRequestHandler<AddFavoriteProductCommand, FavoriteProduct>
    {
        private readonly string _userId;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public AddFavoriteProductCommandHandler(IFavoriteProductRepository favoriteProductRepository)
        {
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<FavoriteProduct> Handle(AddFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            FavoriteProduct favoriteProduct = new FavoriteProduct(_userId, request.ProductId);
            await _favoriteProductRepository.CreateAsync(favoriteProduct);

            return favoriteProduct;
        }
    }
}
