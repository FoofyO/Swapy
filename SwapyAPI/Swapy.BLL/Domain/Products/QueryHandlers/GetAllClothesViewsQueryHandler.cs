using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllClothesViewsQueryHandler : IRequestHandler<GetAllClothesViewsQuery, IEnumerable<ClothesView>>
    {
        private readonly IClothesViewRepository _clothesViewRepository;

        public GetAllClothesViewsQueryHandler(IClothesViewRepository clothesViewRepository) => _clothesViewRepository = clothesViewRepository;

        public async Task<IEnumerable<ClothesView>> Handle(GetAllClothesViewsQuery request, CancellationToken cancellationToken)
        {
            var query = (await _clothesViewRepository.GetQueryableAsync())
                .Where(x => (request.ClothesTypeId == null || x.ClothesTypeId.Equals(request.ClothesTypeId)) &&
                (request.GenderId == null || x.GenderId.Equals(request.GenderId)))
                .OrderBy(x => x.Name);
            var result = await query.ToListAsync();
            return result;
        }
    }
}
