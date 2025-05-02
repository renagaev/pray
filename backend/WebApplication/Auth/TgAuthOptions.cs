using Microsoft.AspNetCore.Authentication;

namespace WebApplication.Auth;

public class TgAuthOptions: AuthenticationSchemeOptions
{
    public string Token { get; set; }
}