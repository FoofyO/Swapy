using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllAutoModelsQueryHandler : IRequestHandler<GetAllAutoModelsQuery, IEnumerable<AutoModel>>
    {
        private readonly IAutoModelRepository _autoModelRepository;

        public GetAllAutoModelsQueryHandler(IAutoModelRepository autoModelRepository) => _autoModelRepository = autoModelRepository;

        public async Task<IEnumerable<AutoModel>> Handle(GetAllAutoModelsQuery request, CancellationToken cancellationToken)
        {
            var result = await _autoModelRepository.GetByBrandsAndTypesAsync(request.AutoBrandsId, request.AutoTypesId);
            return result;
        }
    }
}
