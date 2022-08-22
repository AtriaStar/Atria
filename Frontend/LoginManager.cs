using System.Net.Http.Json;
using Microsoft.JSInterop;
using Models;

namespace Frontend; 

public class LoginManager {
    private readonly HttpClient _httpClient;

    public LoginManager(HttpClient client) {
        _httpClient = client;
    }

    public async Task<User?> GetLoggedInUserAsync() {
        var cnt = await _httpClient.GetAsync("api/auth");
        if (!cnt.IsSuccessStatusCode) { return null; }
        return await cnt.Content.ReadFromJsonAsync<User>();
    }
}
