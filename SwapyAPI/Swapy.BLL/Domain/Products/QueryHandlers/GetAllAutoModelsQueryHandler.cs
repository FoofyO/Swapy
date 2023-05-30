﻿using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllAutoModelsQueryHandler : IRequestHandler<GetAllAutoModelsQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly IAutoModelRepository _autoModelRepository;

        public GetAllAutoModelsQueryHandler(IAutoModelRepository autoModelRepository) => _autoModelRepository = autoModelRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllAutoModelsQuery request, CancellationToken cancellationToken)
        {
            var result = (await _autoModelRepository.GetByBrandsAndTypesAsync(request.AutoBrandsId, request.AutoTypesId)).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
