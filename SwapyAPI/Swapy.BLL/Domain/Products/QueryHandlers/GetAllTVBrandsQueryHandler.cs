using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllTVBrandsQueryHandler : IRequestHandler<GetAllTVBrandsQuery, IEnumerable<TVBrand>>
    {
        private readonly ITVBrandRepository _tvBrandRepository;

        public GetAllTVBrandsQueryHandler(ITVBrandRepository tvBrandRepository) => _tvBrandRepository = tvBrandRepository;

        public async Task<IEnumerable<TVBrand>> Handle(GetAllTVBrandsQuery request, CancellationToken cancellationToken)
        {
            var result = await _tvBrandRepository.GetAllAsync();
            return result;
        }
    }
}
