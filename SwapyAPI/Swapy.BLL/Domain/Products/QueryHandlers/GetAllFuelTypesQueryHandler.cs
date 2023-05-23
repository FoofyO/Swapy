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
    public class GetAllFuelTypesQueryHandler : IRequestHandler<GetAllFuelTypesQuery, IEnumerable<FuelType>>
    {
        private readonly IFuelTypeRepository _fuelTypeRepository;

        public GetAllFuelTypesQueryHandler(IFuelTypeRepository fuelTypeRepository) => _fuelTypeRepository = fuelTypeRepository;

        public async Task<IEnumerable<FuelType>> Handle(GetAllFuelTypesQuery request, CancellationToken cancellationToken)
        {
            var result = await _fuelTypeRepository.GetAllAsync();
            return result;
        }
    }
}
