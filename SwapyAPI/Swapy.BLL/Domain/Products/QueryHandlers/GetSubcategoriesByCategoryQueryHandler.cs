using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetSubcategoriesByCategoryQueryHandler : IRequestHandler<GetSubcategoriesByCategoryQuery, IEnumerable<Subcategory>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetSubcategoriesByCategoryQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<Subcategory>> Handle(GetSubcategoriesByCategoryQuery request, CancellationToken cancellationToken) => await _subcategoryRepository.GetByCategoryAsync(request.CategoryId);
    }
}
