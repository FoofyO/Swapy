using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, IEnumerable<City>>
    {
        private readonly ICityRepository _cityRepository;

        public GetAllCitiesQueryHandler(ICityRepository cityRepository) => _cityRepository = cityRepository;

        public async Task<IEnumerable<City>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var query = (await _cityRepository.GetQueryableAsync()).OrderBy(x => x.Name);
            var result = await query.ToListAsync();
            return result;
        }
    }
}
