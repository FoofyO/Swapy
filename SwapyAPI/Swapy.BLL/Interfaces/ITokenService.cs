using Swapy.Common.Entities;

namespace Swapy.BLL.Interfaces
{
    public interface ITokenService
    {
        Task<Guid> GenerateRefreshToken();
        Task<string> GenerateJwtToken(User user);
    }
}
