using Swapy.Common.Entities;

namespace Swapy.BLL.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}
