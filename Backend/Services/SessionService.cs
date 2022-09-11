using Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.WebUtilities;
using Backend.AspPlugins;

namespace Backend.Services;

public class SessionService {
    public string AuthorizationCookieName => "Authorization";

    public TimeSpan ExpireDuration { get; }

    private readonly string _cookiePath;

    public SessionService(BackendSettings opt) {
        ExpireDuration = opt.AuthenticationTokenExpireDuration;
        _cookiePath = $"/{opt.ApiPrefix}/";
    }

    public bool IsValid(Session session)
        => !session.CreatedAt.OlderThan(ExpireDuration);

    public async Task GenerateSession(User user, AtriaContext context, HttpContext httpContext) {
        var token = Base64UrlTextEncoder.Encode(RandomNumberGenerator.GetBytes(64));

        await context.Sessions.AddAsync(new() {
            Token = token,
            User = user,
            Ip = httpContext.Connection.RemoteIpAddress!.ToString(),
            UserAgent = httpContext.Request.Headers["User-Agent"].ToString(),
        });
        await context.SaveChangesAsync();

        httpContext.Response.Cookies.Append(AuthorizationCookieName, token, new() {
            IsEssential = true,
            HttpOnly = true,
            Secure = true,
            MaxAge = ExpireDuration,
            SameSite = SameSiteMode.Strict,
            Path = _cookiePath,
        });
    }

    public async Task DeleteSession(Session session, AtriaContext context, HttpResponse response) {
        response.Cookies.Delete(AuthorizationCookieName);
        context.Sessions.RemoveRange(context.Sessions.Where(x => x.User == session.User));
        await context.SaveChangesAsync();
    }
}
