using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using Models;

namespace Frontend;

public class LoginState {
    public bool Loaded { get; private set; }

    [MemberNotNullWhen(true, nameof(User))]
    public bool LoggedIn => User != null;

    public User? User { get; private set; }

    private async Task LoadAsync(HttpClient client) {
        var cnt = await client.GetAsync("auth");
        if (cnt.IsSuccessStatusCode) {
           User = await cnt.Content.ReadFromJsonAsync<User>();
        }
        Loaded = true;
    }
    
    public Task Init { get; }
    
    public LoginState(HttpClient client) {
        Init = LoadAsync(client);
    }
}
