using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllScreenResolutionsQueryHandler : IRequestHandler<GetAllScreenResolutionsQuery, IEnumerable<ScreenResolution>>
    {
        private readonly IScreenResolutionRepository _screenResolutionRepository;

        public GetAllScreenResolutionsQueryHandler(IScreenResolutionRepository screenResolutionRepository) => _screenResolutionRepository = screenResolutionRepository;

        public async Task<IEnumerable<ScreenResolution>> Handle(GetAllScreenResolutionsQuery request, CancellationToken cancellationToken)
        {
            var result = await _screenResolutionRepository.GetAllAsync();
            return result;
        }
    }
}
