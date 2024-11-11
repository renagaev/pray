using Microsoft.AspNetCore.Authentication;

namespace WebApplication.Auth;

public class AdminAuthOptions: AuthenticationSchemeOptions
{
    public string Login { get; set; }
    public string Password { get; set; }
}