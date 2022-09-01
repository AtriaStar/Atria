using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using Models;

namespace Frontend;

public class LoginState {

    private readonly HttpClient _httpClient;

    [MemberNotNullWhen(true, nameof(User))]
    public bool LoggedIn => User != null;

    public User? User { get; private set; }
    
    public Task Initialization { get; private set; }

    public LoginState(HttpClient client) {
        _httpClient = client;
        Initialization = CheckAsync();
    }

    private async Task CheckAsync() {
        var cnt = await _httpClient.GetAsync("auth");
        if (cnt.IsSuccessStatusCode) {
           User = await cnt.Content.ReadFromJsonAsync<User>();
        }
    }
}
