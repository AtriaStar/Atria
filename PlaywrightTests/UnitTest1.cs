using Microsoft.Playwright;
using Xunit;

namespace PlaywrightTests;

public class Tests : IAsyncLifetime {
    private readonly string _url = "https://localhost:7206/";

    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;

    public async Task InitializeAsync() {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new() {
#if !CI
            Headless = false,
#endif
        });
    }

    public async Task DisposeAsync() {
        _playwright.Dispose();
        await _browser.DisposeAsync();
    }

    [Fact]
    public async Task RegisterAndLogoutTest() {
        var page = await _browser.NewPageAsync();

        await page.GotoAsync(_url);
        await page.Locator("text=Registrieren").ClickAsync();

        await page.Locator("#lastName").FillAsync("Mustermann");
        await page.Locator("#firstName").FillAsync("Max");
        await page.Locator("input[type=\"email\"]").FillAsync("testemail@mail.com");
        await page.Locator("#password").FillAsync("12345");
        await page.Locator("#confirmPassword").FillAsync("12345");

        await page.Locator("button:has-text(\"Registrieren\")").ClickAsync();
        await page.WaitForURLAsync(_url);

        await page.Locator("[aria-label=\"person circle outline\"]").ClickAsync();
        await page.Locator("text=Abmelden").ClickAsync();
        await page.WaitForURLAsync(_url);
    }

    [Fact]
    public async Task LoginTest() {
        var page = await _browser.NewPageAsync();

        await page.GotoAsync(_url);
        await page.Locator("text=Einloggen").ClickAsync();

        await page.Locator("input[type=\"email\"]").FillAsync("testemail@mail.com");
        await page.Locator("#password").FillAsync("12345");

        await page.Locator("button:has-text(\"Anmelden\")").ClickAsync();
        await page.WaitForURLAsync(_url);
    }
}
