using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApplication.Auth;

public class TgAuthHandler(IOptionsMonitor<TgAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder)
    : AuthenticationHandler<TgAuthOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var headerValue = AuthenticationHeaderValue.Parse(Context.Request.Headers.Authorization);
        var data = HttpUtility.ParseQueryString(headerValue.Parameter);

        var dataDict = new SortedDictionary<string, string>(
            data.AllKeys.ToDictionary(x => x!, x => data[x]!),
            StringComparer.Ordinal);
        var constantKey = "WebAppData";
        var dataCheckString = string.Join(
            '\n', dataDict.Where(x => x.Key != "hash")
                .Select(x => $"{x.Key}={x.Value}"));

        var secretKey = HMACSHA256.HashData(Encoding.UTF8.GetBytes(constantKey), Encoding.UTF8.GetBytes(Options.Token));

        var generatedHash = HMACSHA256.HashData(secretKey, Encoding.UTF8.GetBytes(dataCheckString));

        var actualHash = Convert.FromHexString(dataDict["hash"]);

        if (!actualHash.SequenceEqual(generatedHash))
        {
            return Task.FromResult(AuthenticateResult.Fail("error"));
        }

        var principal = new ClaimsPrincipal(new ClaimsIdentity([], "Tokens"));
        var ticket = new AuthenticationTicket(principal, this.Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}