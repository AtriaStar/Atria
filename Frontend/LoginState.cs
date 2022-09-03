using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using Models;

namespace Frontend;

public class LoginState {
    public bool Loaded { get; private set; }

    [MemberNotNullWhen(true, nameof(User))]
    public bool LoggedIn => Loaded ? User != null : throw new InvalidOperationException("LoginState not loaded yet");

    public User? User { get; private set; }

    public LoginState(HttpClient client, Action trigger) {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        CheckAsync(client, trigger);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    }

    private async Task CheckAsync(HttpClient client, Action trigger) {
        var cnt = await client.GetAsync("auth");
        if (cnt.IsSuccessStatusCode) {
           User = await cnt.Content.ReadFromJsonAsync<User>();
        }
        Loaded = true;
        trigger();
    }
}
