using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllClothesSeasonsQueryHandler : IRequestHandler<GetAllClothesSeasonsQuery, IEnumerable<ClothesSeason>>
    {
        private readonly IClothesSeasonRepository _clothesSeasonRepository;

        public GetAllClothesSeasonsQueryHandler(IClothesSeasonRepository clothesSeasonRepository) => _clothesSeasonRepository = clothesSeasonRepository;

        public async Task<IEnumerable<ClothesSeason>> Handle(GetAllClothesSeasonsQuery request, CancellationToken cancellationToken)
        {
            var result = await _clothesSeasonRepository.GetAllAsync();
            return result;
        }
    }
}
