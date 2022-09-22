using Microsoft.Playwright.NUnit;

namespace PlaywrightTests; 

public class StartTest : PageTest {
    
    [SetUp]
    public async Task Setup() {
        // Go to https://localhost:7206/
        await Page.GotoAsync("https://localhost:7206/");    }

    [Test]
    public async Task WseDetailAndLink() {
        // Click .row > div:nth-child(2) >> nth=0
        await Page.Locator(".row > div:nth-child(2)").First.ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/wse/**");
        // Click text=TwoDuckDuckGo Heike Lena Weber weitere KollaboratorenSearch things >> a
        await Page.Locator("id=wseLink").ClickAsync();
        await Page.WaitForURLAsync("https://**");
    }

    [Test]
    public async Task BookmarkOnLoggedOut() {
        // Click .fa-bookmark >> nth=0
        await Page.Locator(".fa-bookmark").First.ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/login");
    }

    [Test]
    public async Task ApplyOnlineFilter() {
        // Click text=Filter
        await Page.Locator("text=Filter").ClickAsync();
        // Click #tagModal div:has-text("Filter") >> nth=2
        await Page.Locator("#tagModal div:has-text(\"Filter\")").Nth(2).ClickAsync();
        // Check input[type="checkbox"]
        await Page.Locator("input[type=\"checkbox\"]").CheckAsync();
        // Click text=Anwenden
        await Page.Locator("text=Anwenden").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/?IsOnline=True");
    }
}
