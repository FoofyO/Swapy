using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetByIdElectronicAttributeQueryHandler : IRequestHandler<GetByIdElectronicAttributeQuery, ElectronicAttribute>
    {
        private readonly IElectronicAttributeRepository _electronicAttributeRepository;

        public GetByIdElectronicAttributeQueryHandler(IElectronicAttributeRepository electronicAttributeRepository)
        {
            _electronicAttributeRepository = electronicAttributeRepository;
        }

        public async Task<ElectronicAttribute> Handle(GetByIdElectronicAttributeQuery request, CancellationToken cancellationToken)
        {
            return await _electronicAttributeRepository.GetDetailByIdAsync(request.ElectronicAttributeId);
        }
    }
}
