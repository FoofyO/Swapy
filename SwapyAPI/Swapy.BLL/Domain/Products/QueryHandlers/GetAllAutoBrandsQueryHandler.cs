using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllAutoBrandsQueryHandler : IRequestHandler<GetAllAutoBrandsQuery, IEnumerable<AutoBrand>>
    {
        private readonly IAutoBrandRepository _autoBrandRepository;

        public GetAllAutoBrandsQueryHandler(IAutoBrandRepository autoBrandRepository) => _autoBrandRepository = autoBrandRepository;

        public async Task<IEnumerable<AutoBrand>> Handle(GetAllAutoBrandsQuery request, CancellationToken cancellationToken)
        {
            var query = (await _autoBrandRepository.GetQueryableAsync()).OrderBy(x => x.Name);
            var result = await query.ToListAsync();
            return result;
        }
    }
}
