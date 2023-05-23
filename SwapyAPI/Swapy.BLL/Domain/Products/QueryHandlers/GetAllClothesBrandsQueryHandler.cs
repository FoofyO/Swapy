using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllClothesBrandsQueryHandler : IRequestHandler<GetAllClothesBrandsQuery, IEnumerable<ClothesBrand>>
    {
        private readonly IClothesBrandRepository _clothesBrandRepository;

        public GetAllClothesBrandsQueryHandler(IClothesBrandRepository clothesBrandRepository) => _clothesBrandRepository = clothesBrandRepository;

        public async Task<IEnumerable<ClothesBrand>> Handle(GetAllClothesBrandsQuery request, CancellationToken cancellationToken)
        {
            var result = await _clothesBrandRepository.GetByClothesViewsAsync(request.ClothesViewsId);
            return result;
        }
    }
}
