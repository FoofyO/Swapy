using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandler
{
    public class GetByIdItemAttributeQueryHandler : IRequestHandler<GetByIdItemAttributeQuery, ItemAttribute>
    {
        private readonly IItemAttributeRepository _itemAttributeRepository;

        public GetByIdItemAttributeQueryHandler(IItemAttributeRepository itemAttributeRepository)
        {
            _itemAttributeRepository = itemAttributeRepository;
        }

        public async Task<ItemAttribute> Handle(GetByIdItemAttributeQuery request, CancellationToken cancellationToken)
        {
            return await _itemAttributeRepository.GetDetailByIdAsync(request.ItemAttributeId);
        }
    }
}
