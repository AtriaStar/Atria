using Microsoft.Playwright;

namespace PlaywrightTests;

public class Tests {

    [Test]
    public async Task RegisterAndLogout() {

        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions {
            Headless = false
        });
        var page = await browser.NewPageAsync();

        await page.GotoAsync("https://localhost:7206/");
        await page.Locator("text=Registrieren").ClickAsync();

        await page.Locator("#lastName").FillAsync("Mustermann");
        await page.Locator("#firstName").FillAsync("Max");
        await page.Locator("input[type=\"email\"]").FillAsync("testemail@mail.com");
        await page.Locator("#password").FillAsync("12345");
        await page.Locator("#confirmPassword").FillAsync("12345");

        await page.Locator("button:has-text(\"Registrieren\")").ClickAsync();
        await page.WaitForURLAsync("https://localhost:7206/");

        await page.Locator("[aria-label=\"person circle outline\"]").ClickAsync();
        await page.Locator("text=Abmelden").ClickAsync();
        await page.WaitForURLAsync("https://localhost:7206/");
    }
}
