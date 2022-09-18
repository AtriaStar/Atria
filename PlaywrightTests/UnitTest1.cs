using Microsoft.Playwright;

namespace PlaywrightTests;

public class Tests {

    public IBrowser Browser { get; set; }
    private readonly string _url = "https://localhost:7206/";

    [OneTimeSetUp]
    public async Task SetupBrowser() {
        using var playwright = await Playwright.CreateAsync();
        Browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions {Headless = false});
    }

    [Test]
    public async Task RegisterAndLogoutTest() { 
        var page = await Browser.NewPageAsync();

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

    [Test]
    public async Task LoginTest() {
        var page = await Browser.NewPageAsync();

        await page.GotoAsync(_url);
        await page.Locator("text=Einloggen").ClickAsync();

        await page.Locator("input[type=\"email\"]").FillAsync("testemail@mail.com");
        await page.Locator("#password").FillAsync("12345");

        await page.Locator("button:has-text(\"Anmelden\")").ClickAsync();
        await page.WaitForURLAsync(_url);
    }

}