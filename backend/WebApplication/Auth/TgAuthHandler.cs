using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot.Extensions.LoginWidget;

namespace WebApplication.Auth;

public class TgAuthHandler(IOptionsMonitor<TgAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder)
    : AuthenticationHandler<TgAuthOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var headerValue = Context.Request.Headers.Authorization;
        var widget = new LoginWidget(Options.Token);

        if (headerValue.Count == 0)
        {
            return Task.FromResult(AuthenticateResult.Fail("No authorization header"));
        }

        return Task.FromResult(AuthenticateResult.Fail("error"));
    }
}