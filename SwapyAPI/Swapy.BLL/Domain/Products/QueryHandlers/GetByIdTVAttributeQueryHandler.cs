using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
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
