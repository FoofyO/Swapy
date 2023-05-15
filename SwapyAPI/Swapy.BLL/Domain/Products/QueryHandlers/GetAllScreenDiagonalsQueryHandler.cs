using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllScreenDiagonalsQueryHandler : IRequestHandler<GetAllScreenDiagonalsQuery, IEnumerable<ScreenDiagonal>>
    {
        private readonly IScreenDiagonalRepository _screenDiagonalRepository;

        public GetAllScreenDiagonalsQueryHandler(IScreenDiagonalRepository screenDiagonalRepository) => _screenDiagonalRepository = screenDiagonalRepository;

        public async Task<IEnumerable<ScreenDiagonal>> Handle(GetAllScreenDiagonalsQuery request, CancellationToken cancellationToken)
        {
            var query = (await _screenDiagonalRepository.GetQueryableAsync()).OrderBy(x => x.Name);
            var result = await query.ToListAsync();
            return result;
        }
    }
}
