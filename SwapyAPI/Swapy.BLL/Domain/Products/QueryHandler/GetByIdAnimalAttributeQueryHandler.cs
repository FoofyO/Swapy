using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandler
{
    public class GetByIdAnimalAttributeQueryHandler : IRequestHandler<GetByIdAnimalAttributeQuery, AnimalAttribute>
    {
        private readonly IAnimalAttributeRepository _animalAttributeRepository;

        public GetByIdAnimalAttributeQueryHandler(IAnimalAttributeRepository animalAttributeRepository)
        {
            _animalAttributeRepository = animalAttributeRepository;
        }

        public async Task<AnimalAttribute> Handle(GetByIdAnimalAttributeQuery request, CancellationToken cancellationToken)
        {
            return await _animalAttributeRepository.GetDetailByIdAsync(request.AnimalAttributeId);
        }
    }
}
