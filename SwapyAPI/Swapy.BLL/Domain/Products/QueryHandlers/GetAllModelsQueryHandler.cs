﻿using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllModelsQueryHandler : IRequestHandler<GetAllModelsQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly IModelRepository _modelRepository;

        public GetAllModelsQueryHandler(IModelRepository modelRepository) => _modelRepository = modelRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllModelsQuery request, CancellationToken cancellationToken)
        {
            var result = (await _modelRepository.GetByBrandsAndTypeAsync(request.ElectronicBrandsId, request.ElectronicTypeId)).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
