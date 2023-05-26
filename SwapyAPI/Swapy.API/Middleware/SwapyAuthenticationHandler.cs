using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Swapy.DAL.Interfaces;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Swapy.API.Middleware
{
    public class SwapyAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private readonly IUserTokenRepository _userTokenRepository;

        public SwapyAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOptions> options,
                                          ILoggerFactory logger,
                                          UrlEncoder encoder,
                                          ISystemClock clock,
                                          IUserTokenRepository userTokenRepository) : base(options, logger, encoder, clock) => _userTokenRepository = userTokenRepository;

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization")) return AuthenticateResult.Fail("Unauthorized");

            string authorizationHeader = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader)) return AuthenticateResult.NoResult();

            if (!authorizationHeader.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase)) return AuthenticateResult.Fail("Unauthorized");

            string token = authorizationHeader.Substring("Bearer".Length).Trim();

            if (string.IsNullOrEmpty(token)) return AuthenticateResult.Fail("Unauthorized");

            try
            {
                return await ValidateToken(token);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }

        private async Task<AuthenticateResult> ValidateToken(string accessToken)
        {
            var userToken = await _userTokenRepository.GetByAccessTokenAsync(accessToken);
            
            if(userToken == null) return AuthenticateResult.Fail("Unauthorized");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Hash, userToken.AccessToken),
                new Claim(ClaimTypes.NameIdentifier, userToken.UserId),
                new Claim(ClaimTypes.Authentication, userToken.RefreshToken),
                new Claim(ClaimTypes.Role, userToken.User.Type.ToString())
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
