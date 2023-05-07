using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandler
{
    public class GetByIdClothesAttributeQueryHandler : IRequestHandler<GetByIdClothesAttributeQuery, ClothesAttribute>
    {
        private readonly IClothesAttributeRepository _clothesAttributeRepository;

        public GetByIdClothesAttributeQueryHandler(IClothesAttributeRepository clothesAttributeRepository)
        {
            _clothesAttributeRepository = clothesAttributeRepository;
        }

        public async Task<ClothesAttribute> Handle(GetByIdClothesAttributeQuery request, CancellationToken cancellationToken)
        {
            return await _clothesAttributeRepository.GetDetailByIdAsync(request.ClothesAttributeId);
        }
    }
}
