using MediatR;
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
    public class GetSubcategoriesBySubcategoryQueryHandler : IRequestHandler<GetSubcategoriesBySubcategoryQuery, IEnumerable<Subcategory>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetSubcategoriesBySubcategoryQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<Subcategory>> Handle(GetSubcategoriesBySubcategoryQuery request, CancellationToken cancellationToken) => await _subcategoryRepository.GetBySubcategoryAsync(request.SubcategoryId);
    }
}
