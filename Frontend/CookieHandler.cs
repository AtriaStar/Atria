﻿using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace Frontend; 

public class CookieHandler : DelegatingHandler {
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include); // TODO: Same origin?
        return await base.SendAsync(request, cancellationToken);
    }
}