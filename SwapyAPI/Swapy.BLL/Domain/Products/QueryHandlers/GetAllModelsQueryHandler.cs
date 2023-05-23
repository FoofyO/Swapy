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
            var result = await _modelRepository.GetByBrandsAndTypeAsync(request.ElectronicBrandsId, request.ElectronicTypeId);
            return result;
        }
    }
}
