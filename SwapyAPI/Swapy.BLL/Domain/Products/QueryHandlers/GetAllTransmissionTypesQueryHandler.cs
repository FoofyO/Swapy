using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllTransmissionTypesQueryHandler : IRequestHandler<GetAllTransmissionTypesQuery, IEnumerable<TransmissionType>>
    {
        private readonly ITransmissionTypeRepository _transmissionTypeRepository;

        public GetAllTransmissionTypesQueryHandler(ITransmissionTypeRepository transmissionTypeRepository) => _transmissionTypeRepository = transmissionTypeRepository;

        public async Task<IEnumerable<TransmissionType>> Handle(GetAllTransmissionTypesQuery request, CancellationToken cancellationToken)
        {
            var result = await _transmissionTypeRepository.GetAllAsync();
            return result;
        }
    }
}
