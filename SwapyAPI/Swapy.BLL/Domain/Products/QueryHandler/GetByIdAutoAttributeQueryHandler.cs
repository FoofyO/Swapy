using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandler
{
    public class GetByIdAutoAttributeQueryHandler : IRequestHandler<GetByIdAutoAttributeQuery, AutoAttribute>
    {
        private readonly IAutoAttributeRepository _autoAttributeRepository;

        public GetByIdAutoAttributeQueryHandler(IAutoAttributeRepository autoAttributeRepository)
        {
            _autoAttributeRepository = autoAttributeRepository;
        }

        public async Task<AutoAttribute> Handle(GetByIdAutoAttributeQuery request, CancellationToken cancellationToken)
        {
            return await _autoAttributeRepository.GetDetailByIdAsync(request.AutoAttributeId);
        }
    }
}
