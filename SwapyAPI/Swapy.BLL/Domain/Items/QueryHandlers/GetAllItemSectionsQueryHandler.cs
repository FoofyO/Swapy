using MediatR;
using Swapy.BLL.Domain.Items.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Items.QueryHandlers
{
    public class GetAllItemSectionsQueryHandler : IRequestHandler<GetAllItemSectionsQuery, IEnumerable<SpecificationResponseDTO>>
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public GetAllItemSectionsQueryHandler(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;

        public async Task<IEnumerable<SpecificationResponseDTO>> Handle(GetAllItemSectionsQuery request, CancellationToken cancellationToken)
        {
            var result = (await _subcategoryRepository.GetAllItemSectionsAsync()).Select(x => new SpecificationResponseDTO(x.Id, x.Name));
            return result;
        }
    }
}
