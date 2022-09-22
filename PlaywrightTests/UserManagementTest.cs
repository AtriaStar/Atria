using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

public class UserManagementTest : PageTest {
    private string _email = null!;
    private string _password = null!;
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz012345789";

    [SetUp]
    public void Setup() {
        var random = new Random();
        var emailFirst = new string(Enumerable.Repeat(Chars, 10)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        var emailSecond = new string(Enumerable.Repeat(Chars, 10)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        _email = emailFirst + "@" + emailSecond;
        _password = new string(Enumerable.Repeat(Chars, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    [Test]
    public async Task ValidRegister() {
        // Go to https://localhost:7206/register
        await Page.GotoAsync("https://localhost:7206/register");
        // Click #lastName
        await Page.Locator("#lastName").ClickAsync();
        // Fill #lastName
        await Page.Locator("#lastName").FillAsync("test");
        // Click #firstName
        await Page.Locator("#firstName").ClickAsync();
        // Fill #firstName
        await Page.Locator("#firstName").FillAsync("test");
        // Click input[type="email"]
        await Page.Locator("input[type=\"email\"]").ClickAsync();
        // Fill input[type="email"]
        await Page.Locator("input[type=\"email\"]").FillAsync(_email);
        // Click #password
        await Page.Locator("#password").ClickAsync();
        // Fill #password
        await Page.Locator("#password").FillAsync("teste");
        // Click #confirmPassword
        await Page.Locator("#confirmPassword").ClickAsync();
        // Fill #confirmPassword
        await Page.Locator("#confirmPassword").FillAsync("teste");
        // Click button:has-text("Registrieren")
        await Page.Locator("button:has-text(\"Registrieren\")").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/");
    }

    [Test]
    public async Task RegisterLogoutLogin() {
        // Go to https://localhost:7206/register
        await Page.GotoAsync("https://localhost:7206/register");
        // Click #lastName
        await Page.Locator("#lastName").ClickAsync();
        // Fill #lastName
        await Page.Locator("#lastName").FillAsync("test");
        // Press Tab
        await Page.Locator("#lastName").PressAsync("Tab");
        // Fill #firstName
        await Page.Locator("#firstName").FillAsync("test");
        // Press Tab
        await Page.Locator("#firstName").PressAsync("Tab");
        // Fill input[type="email"]
        await Page.Locator("input[type=\"email\"]").FillAsync(_email);
        // Press Tab
        await Page.Locator("input[type=\"email\"]").PressAsync("Tab");
        // Fill #password
        await Page.Locator("#password").FillAsync(_password);
        // Press Tab
        await Page.Locator("#password").PressAsync("Tab");
        // Fill #confirmPassword
        await Page.Locator("#confirmPassword").FillAsync(_password);
        // Click button:has-text("Registrieren")
        await Page.Locator("button:has-text(\"Registrieren\")").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/");
        // Click [aria-label="person circle outline"]
        await Page.Locator("[aria-label=\"person circle outline\"]").ClickAsync();
        // Click text=Abmelden
        await Page.Locator("text=Abmelden").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/");
        // Click text=Einloggen
        await Page.Locator("text=Einloggen").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/login?returnurl=");
        // Click input[type="email"]
        await Page.Locator("input[type=\"email\"]").ClickAsync();
        // Fill input[type="email"]
        await Page.Locator("input[type=\"email\"]").FillAsync(_email);
        // Click input[type="password"]
        await Page.Locator("input[type=\"password\"]").ClickAsync();
        // Fill input[type="password"]
        await Page.Locator("input[type=\"password\"]").FillAsync(_password);
        // Click text=Anmelden
        await Page.Locator("text=Anmelden").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/");
    }

        // Fill input[type="password"]
        await Page.Locator("input[type=\"password\"]").FillAsync(_password + "asdf");
        // Click text=Anmelden
        await Page.Locator("text=Anmelden").ClickAsync();
        // Click text=Email or password invalid
        await Page.Locator("text=Email or password invalid").ClickAsync();
    }
    
    
}
