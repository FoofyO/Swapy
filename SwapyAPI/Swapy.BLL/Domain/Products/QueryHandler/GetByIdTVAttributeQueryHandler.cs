using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.Products.QueryHandler
{
    public class GetByIdTVAttributeQueryHandler : IRequestHandler<GetByIdTVAttributeQuery, TVAttribute>
    {
        private readonly ITVAttributeRepository _tvAttributeRepository;

        public GetByIdTVAttributeQueryHandler(ITVAttributeRepository tvAttributeRepository)
        {
            _tvAttributeRepository = tvAttributeRepository;
        }

        public async Task<TVAttribute> Handle(GetByIdTVAttributeQuery request, CancellationToken cancellationToken)
        {
            return await _tvAttributeRepository.GetDetailByIdAsync(request.TVAttributeId);
        }
    }
}
