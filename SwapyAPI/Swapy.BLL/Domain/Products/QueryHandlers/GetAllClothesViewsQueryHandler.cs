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
            var result = await _clothesViewRepository.GetByGenderAndTypeAsync(request.GenderId, request.ClothesTypeId);
            return result;
        }
    }
}
