using System.Net.Http.Json;
using Microsoft.JSInterop;
using Models;

namespace Frontend; 

public class LoginManager {
    private readonly HttpClient HttpClient;
    private readonly IJSRuntime JS;

    public LoginManager(HttpClient client, IJSRuntime js) {
        HttpClient = client;
        JS = js;
    }

    public ValueTask<bool> IsLoggedInAsync()
        => JS.InvokeAsync<bool>("checkCookie", "Authorization");

    public async Task<User?> GetLoggedInUserAsync() {
        if (!await IsLoggedInAsync()) { return null; }
        var cnt = await HttpClient.GetAsync("api/auth");
        if (!cnt.IsSuccessStatusCode) { return null; }
        return await cnt.Content.ReadFromJsonAsync<User>();
    }
}
