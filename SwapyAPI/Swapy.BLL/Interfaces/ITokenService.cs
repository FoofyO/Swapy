using Swapy.Common.Entities;

namespace Swapy.BLL.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateRefreshToken();
        Task<string> GenerateJwtToken(User user);
    }
}
