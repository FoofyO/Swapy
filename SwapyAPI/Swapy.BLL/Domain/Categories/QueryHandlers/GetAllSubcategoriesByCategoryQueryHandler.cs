using MediatR;
using Swapy.BLL.Domain.Categories.Queries;
using Swapy.Common.DTO;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.BLL.Domain.Categories.QueryHandlers
{
    public class GetAllSubcategoriesByCategoryQueryHandler : IRequestHandler<GetAllSubcategoriesByCategoryQuery, IEnumerable<Subcategory>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllSubcategoriesByCategoryQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<Subcategory>> Handle(GetAllSubcategoriesByCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _subcategoryRepository.GetByCategoryAsync(request.CategoryId);
        }
    }
}
