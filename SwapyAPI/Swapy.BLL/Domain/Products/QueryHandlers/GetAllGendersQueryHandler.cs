using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllGendersQueryHandler : IRequestHandler<GetAllGendersQuery, IEnumerable<Gender>>
    {
        private readonly IGenderRepository _genderRepository;

        public GetAllGendersQueryHandler(IGenderRepository genderRepository) => _genderRepository = genderRepository;

        public async Task<IEnumerable<Gender>> Handle(GetAllGendersQuery request, CancellationToken cancellationToken)
        {
            var result = await _genderRepository.GetAllAsync();
            return result;
        }
    }
}
