using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApplication.Auth;

public class AdminAuthHandler(IOptionsMonitor<AdminAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder)
    : AuthenticationHandler<AdminAuthOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var headerValue = Context.Request.Headers.Authorization.ToString();
        if (headerValue == $"{Options.Login}:{Options.Password}")
        {
            var principal = new ClaimsPrincipal(new ClaimsIdentity([], "Tokens"));
            var ticket = new AuthenticationTicket(principal, this.Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        return Task.FromResult(AuthenticateResult.Fail("error"));
    }
}