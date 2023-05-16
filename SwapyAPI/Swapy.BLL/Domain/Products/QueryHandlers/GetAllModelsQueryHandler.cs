using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllModelsQueryHandler : IRequestHandler<GetAllModelsQuery, IEnumerable<Model>>
    {
        private readonly IModelRepository _modelRepository;

        public GetAllModelsQueryHandler(IModelRepository modelRepository) => _modelRepository = modelRepository;

        public async Task<IEnumerable<Model>> Handle(GetAllModelsQuery request, CancellationToken cancellationToken)
        {
            var query = (await _modelRepository.GetQueryableAsync())
                .Include(x => x.ElectronicBrandType)
                .Where(x => (request.ElectronicBrandId == null || x.ElectronicBrandType.ElectronicBrandId.Equals(request.ElectronicBrandId)) &&
                (request.ElectronicTypeId == null || x.ElectronicBrandType.ElectronicTypeId.Equals(request.ElectronicTypeId)))
                .OrderBy(x => x.Name);
            var result = await query.ToListAsync();
            return result;
        }
    }
}
