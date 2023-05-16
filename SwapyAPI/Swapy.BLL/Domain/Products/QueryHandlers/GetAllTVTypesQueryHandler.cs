using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllTVTypesQueryHandler : IRequestHandler<GetAllTVTypesQuery, IEnumerable<TVType>>
    {
        private readonly ITVTypeRepository _tvTypeRepository;

        public GetAllTVTypesQueryHandler(ITVTypeRepository tvTypeRepository) => _tvTypeRepository = tvTypeRepository;

        public async Task<IEnumerable<TVType>> Handle(GetAllTVTypesQuery request, CancellationToken cancellationToken)
        {
            var query = (await _tvTypeRepository.GetQueryableAsync()).OrderBy(x => x.Name);
            var result = await query.ToListAsync();
            return result;
        }
    }
}
