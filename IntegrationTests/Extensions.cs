using Xunit.Sdk;

namespace IntegrationTests; 

public static class Extensions {
    public static async Task AssertStatusCode(this HttpResponseMessage resp) {
        if (resp.IsSuccessStatusCode) {
            return;
        }
        
        throw new XunitException($"{resp.StatusCode}: {await resp.Content.ReadAsStringAsync()}");
    }
}
