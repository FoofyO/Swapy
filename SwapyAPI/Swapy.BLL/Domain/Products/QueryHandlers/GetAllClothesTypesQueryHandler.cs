using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllClothesTypesQueryHandler : IRequestHandler<GetAllClothesTypesQuery, IEnumerable<Subcategory>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllClothesTypesQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<Subcategory>> Handle(GetAllClothesTypesQuery request, CancellationToken cancellationToken)
        {
            var result = await _subcategoryRepository.GetClothesTypesByGenderAsync(request.GenderId);
            return result;
        }
    }
}
