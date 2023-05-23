using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IColorRepository : IRepository<Color>
    {
        Task<IEnumerable<Color>> GetByModelAsync(string modelId);
    }
}
