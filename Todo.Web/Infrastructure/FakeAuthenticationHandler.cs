using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Todo.Web.Infrastructure
{
    public class FakeAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var userIdHeader = Request.Headers["X-User-Id"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(userIdHeader) || !int.TryParse(userIdHeader, out var userId))
            {
                return Task.FromResult(AuthenticateResult.Fail("Missing or invalid X-User-Id header."));
            }

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
