using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using Models;

namespace Frontend;

public class LoginState {
    public bool Fetched { get; private set; }

    [MemberNotNullWhen(true, nameof(User))]
    public bool LoggedIn => User != null;

    public User? User { get; private set; }

    public LoginState(HttpClient client) {
#pragma warning disable CS4014
        Check(client);
#pragma warning restore CS4014
    }

    private async Task Check(HttpClient client) {
        var cnt = await client.GetAsync("auth");
        if (cnt.IsSuccessStatusCode) {
            User = await cnt.Content.ReadFromJsonAsync<User>();
        }
        Fetched = true;
    }
}
