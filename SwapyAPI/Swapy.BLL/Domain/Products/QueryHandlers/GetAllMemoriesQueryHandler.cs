using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllMemoriesQueryHandler : IRequestHandler<GetAllMemoriesQuery, IEnumerable<Memory>>
    {
        private readonly IMemoryRepository _memoryRepository;

        public GetAllMemoriesQueryHandler(IMemoryRepository memoryRepository) => _memoryRepository = memoryRepository;

        public async Task<IEnumerable<Memory>> Handle(GetAllMemoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _memoryRepository.GetByModelAsync(request.ModelId);
            return result;
        }
    }
}
