using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IAnimalBreedRepository : IRepository<AnimalBreed>
    {
        Task<IEnumerable<AnimalBreed>> GetByAnimalTypeAsync(string animalType);
    } 
}
 