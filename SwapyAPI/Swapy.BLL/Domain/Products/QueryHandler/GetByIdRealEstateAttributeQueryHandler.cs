using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandler
{
    public class GetByIdRealEstateAttributeQueryHandler : IRequestHandler<GetByIdRealEstateAttributeQuery, RealEstateAttribute>
    {
        private readonly IRealEstateAttributeRepository _realEstateAttributeRepository;

        public GetByIdRealEstateAttributeQueryHandler(IRealEstateAttributeRepository realEstateAttributeRepository)
        {
            _realEstateAttributeRepository = realEstateAttributeRepository;
        }

        public async Task<RealEstateAttribute> Handle(GetByIdRealEstateAttributeQuery request, CancellationToken cancellationToken)
        {
            return await _realEstateAttributeRepository.GetDetailByIdAsync(request.RealEstateAttributeId);
        }
    }
}
