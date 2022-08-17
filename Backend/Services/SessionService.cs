using Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.WebUtilities;

namespace Backend.Services; 

public class SessionService {
    public string AuthorizationCookieName => "Authorization";

    public TimeSpan ExpireDuration { get; } = TimeSpan.FromDays(7);

    public bool IsValid(Session session)
        => DateTimeOffset.UtcNow - session.CreatedAt <= ExpireDuration;

    public async Task GenerateSession(User user, AtriaContext context, HttpContext httpContext) {
        var token = Base64UrlTextEncoder.Encode(RandomNumberGenerator.GetBytes(64));

        await context.Sessions.AddAsync(new() {
            Token = token,
            User = user,
            Ip = httpContext.Connection.RemoteIpAddress!.ToString(),
            UserAgent = httpContext.Request.Headers["User-Agent"],
        });
        await context.SaveChangesAsync();

        httpContext.Response.Cookies.Append(AuthorizationCookieName, token, new() {
            IsEssential = true,
            HttpOnly = true,
            Secure = true,
            MaxAge = ExpireDuration,
            SameSite = SameSiteMode.Strict,
            Path = "/api/",
        });
    }

    public async Task DeleteSession(Session session, AtriaContext context, HttpResponse response) {
        response.Cookies.Delete(AuthorizationCookieName);
        context.Sessions.RemoveRange(context.Sessions.Where(x => x.User == session.User));
        await context.SaveChangesAsync();
    }
}
