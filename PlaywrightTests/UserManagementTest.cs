using Microsoft.Playwright;

namespace PlaywrightTests; 

public class UserManagementTest {

    [Test]
    public async Task ValidRegister() {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz012345789";
        var random = new Random();
        var email = new string(Enumerable.Repeat(chars, 10)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        var emailSecond = new string(Enumerable.Repeat(chars, 10)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
        });
        var context = await browser.NewContextAsync();
        var page = await browser.NewPageAsync();

        // Go to https://localhost:7206/
        await page.GotoAsync("https://localhost:7206/");
        // Click text=Registrieren
        await page.Locator("text=Registrieren").ClickAsync();
        await page.WaitForURLAsync("https://localhost:7206/register");
        // Click #lastName
        await page.Locator("#lastName").ClickAsync();
        // Fill #lastName
        await page.Locator("#lastName").FillAsync("test");
        // Click #firstName
        await page.Locator("#firstName").ClickAsync();
        // Fill #firstName
        await page.Locator("#firstName").FillAsync("test");
        // Click input[type="email"]
        await page.Locator("input[type=\"email\"]").ClickAsync();
        // Fill input[type="email"]
        await page.Locator("input[type=\"email\"]").FillAsync(email+"@"+emailSecond);
        // Click #password
        await page.Locator("#password").ClickAsync();
        // Fill #password
        await page.Locator("#password").FillAsync("teste");
        // Click #confirmPassword
        await page.Locator("#confirmPassword").ClickAsync();
        // Fill #confirmPassword
        await page.Locator("#confirmPassword").FillAsync("teste");
        // Click button:has-text("Registrieren")
        await page.Locator("button:has-text(\"Registrieren\")").ClickAsync();
        await page.WaitForURLAsync("https://localhost:7206/");
    }

    [Test]
    public async Task RegisterLogoutLogin() {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz012345789";
        var random = new Random();
        var email = new string(Enumerable.Repeat(chars, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        var emailSecond = new string(Enumerable.Repeat(chars, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        var password = new string(Enumerable.Repeat(chars, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
        });
        var context = await browser.NewContextAsync();
        var page = await browser.NewPageAsync();


        // Go to https://localhost:7206/register
        await page.GotoAsync("https://localhost:7206/register");

        // Click #lastName
        await page.Locator("#lastName").ClickAsync();

        // Fill #lastName
        await page.Locator("#lastName").FillAsync("test");

        // Press Tab
        await page.Locator("#lastName").PressAsync("Tab");

        // Fill #firstName
        await page.Locator("#firstName").FillAsync("test");

        // Press Tab
        await page.Locator("#firstName").PressAsync("Tab");

        // Fill input[type="email"]
        await page.Locator("input[type=\"email\"]").FillAsync(email+"@"+emailSecond);

        // Press Tab
        await page.Locator("input[type=\"email\"]").PressAsync("Tab");

        // Fill #password
        await page.Locator("#password").FillAsync(password);

        // Press Tab
        await page.Locator("#password").PressAsync("Tab");

        // Fill #confirmPassword
        await page.Locator("#confirmPassword").FillAsync(password);

        // Click button:has-text("Registrieren")
        await page.Locator("button:has-text(\"Registrieren\")").ClickAsync();
        await page.WaitForURLAsync("https://localhost:7206/");
        
        // Click [aria-label="person circle outline"]
        await page.Locator("[aria-label=\"person circle outline\"]").ClickAsync();

        // Click text=Abmelden
        await page.Locator("text=Abmelden").ClickAsync();
        await page.WaitForURLAsync("https://localhost:7206/");

        // Click text=Einloggen
        await page.Locator("text=Einloggen").ClickAsync();
        await page.WaitForURLAsync("https://localhost:7206/login?returnurl=");

        // Click input[type="email"]
        await page.Locator("input[type=\"email\"]").ClickAsync();

        // Fill input[type="email"]
        await page.Locator("input[type=\"email\"]").FillAsync(email+"@"+emailSecond);

        // Click input[type="password"]
        await page.Locator("input[type=\"password\"]").ClickAsync();

        // Fill input[type="password"]
        await page.Locator("input[type=\"password\"]").FillAsync(password);

        // Click text=Anmelden
        await page.Locator("text=Anmelden").ClickAsync();
        await page.WaitForURLAsync("https://localhost:7206/");
        
        // Save storage state into the file.
        await context.StorageStateAsync(new()
        {
            Path = "state.json"
        });
    }
    
    
}
