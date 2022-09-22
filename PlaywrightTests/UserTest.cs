using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

[TestFixture]
public class UserTest : PageTest {
    private string _email = null!;
    private string _emailSecond = null!;
    private string _password = null!;
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz012345789";


    [SetUp]
    public async Task Setup() {
        var random = new Random();
        _email = new string(Enumerable.Repeat(Chars, 10)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        _emailSecond = new string(Enumerable.Repeat(Chars, 10)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        _password = new string(Enumerable.Repeat(Chars, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        // Go to https://localhost:7206/
        await Page.GotoAsync("https://localhost:7206/");
        // Click text=Registrieren
        await Page.Locator("text=Registrieren").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/register");
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
        await Page.Locator("input[type=\"email\"]").FillAsync(_email + "@" + _emailSecond);
        // Click #password
        await Page.Locator("#password").ClickAsync();
        // Fill #password
        await Page.Locator("#password").FillAsync(_password);
        // Click #confirmPassword
        await Page.Locator("#confirmPassword").ClickAsync();
        // Fill #confirmPassword
        await Page.Locator("#confirmPassword").FillAsync(_password);
        // Click button:has-text("Registrieren")
        await Page.Locator("button:has-text(\"Registrieren\")").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/");
    }

    [Test]
    public async Task CreateAndDeleteWse() {
        // Click [aria-label="add circle outline"]
        await Page.Locator("[aria-label=\"add circle outline\"]").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/newWse");
        // Click #name
        await Page.Locator("#name").ClickAsync();
        // Fill #name
        await Page.Locator("#name").FillAsync("test");
        // Click #shortDescription
        await Page.Locator("#shortDescription").ClickAsync();
        // Fill #shortDescription
        await Page.Locator("#shortDescription").FillAsync("test");
        // Click [placeholder="http\(s\)\:\/\/\.\.\."]
        await Page.Locator("[placeholder=\"http\\(s\\)\\:\\/\\/\\.\\.\\.\"]").ClickAsync();
        // Fill [placeholder="http\(s\)\:\/\/\.\.\."]
        await Page.Locator("[placeholder=\"http\\(s\\)\\:\\/\\/\\.\\.\\.\"]").FillAsync("https://testen.de");
        // Click text=Veröffentlichen
        await Page.Locator("text=Veröffentlichen").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/wse/**");
        // Click h3:has-text("test")
        await Page.Locator("h3:has-text(\"test\")").ClickAsync();
        // Click text=Löschen >> nth=0
        await Page.Locator("text=Löschen").First.ClickAsync();
        // Click div[role="dialog"] button:has-text("Löschen")
        await Page.Locator("div[role=\"dialog\"] button:has-text(\"Löschen\")").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/");
    }

    [Test]
    public async Task EditProfile() {
        // Click [aria-label="person circle outline"]
        await Page.Locator("[aria-label=\"person circle outline\"]").ClickAsync();
        // Click text=Mein Konto
        await Page.Locator("text=Mein Konto").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/profile/**");
        // Click text=Profil bearbeiten
        await Page.Locator("text=Profil bearbeiten").ClickAsync();
        // Click [placeholder="Vorname"]
        await Page.Locator("[placeholder=\"Vorname\"]").ClickAsync();
        // Press a with modifiers
        await Page.Locator("[placeholder=\"Vorname\"]").PressAsync("Control+a");
        // Fill [placeholder="Vorname"]
        await Page.Locator("[placeholder=\"Vorname\"]").FillAsync("Hans");
        // Press Tab
        await Page.Locator("[placeholder=\"Vorname\"]").PressAsync("Tab");
        // Fill [placeholder="Nachname"]
        await Page.Locator("[placeholder=\"Nachname\"]").FillAsync("Gustav");
        // Press Enter
        await Page.Locator("[placeholder=\"Nachname\"]").PressAsync("Enter");
        // Click text=Hans Gustav
        await Page.Locator("text=Hans Gustav").ClickAsync();
    }

    [Test]
    public async Task CreateWseAndEditViaProfile() {
        // Click [aria-label="add circle outline"]
        await Page.Locator("[aria-label=\"add circle outline\"]").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/newWse");
        // Click #name
        await Page.Locator("#name").ClickAsync();
        // Fill #name
        await Page.Locator("#name").FillAsync("Test");
        // Click #shortDescription
        await Page.Locator("#shortDescription").ClickAsync();
        // Fill #shortDescription
        await Page.Locator("#shortDescription").FillAsync("Test");
        // Click [placeholder="http\(s\)\:\/\/\.\.\."]
        await Page.Locator("[placeholder=\"http\\(s\\)\\:\\/\\/\\.\\.\\.\"]").ClickAsync();
        // Fill [placeholder="http\(s\)\:\/\/\.\.\."]
        await Page.Locator("[placeholder=\"http\\(s\\)\\:\\/\\/\\.\\.\\.\"]").FillAsync("https://teste.de");
        // Click text=Veröffentlichen
        await Page.Locator("text=Veröffentlichen").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/wse/**");
        // Click [aria-label="person circle outline"]
        await Page.Locator("[aria-label=\"person circle outline\"]").ClickAsync();
        // Click text=Mein Konto
        await Page.Locator("text=Mein Konto").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/profile/**");
        // Click a:has-text("Test")
        await Page.Locator("a:has-text(\"Test\")").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/wse/**");
        // Click text=Bearbeiten >> nth=0
        await Page.Locator("text=Bearbeiten").First.ClickAsync();
        // Click [placeholder="Name"]
        await Page.Locator("[placeholder=\"Name\"]").ClickAsync();
        // Press a with modifiers
        await Page.Locator("[placeholder=\"Name\"]").PressAsync("Control+a");
        // Fill [placeholder="Name"]
        await Page.Locator("[placeholder=\"Name\"]").FillAsync("Der Test");
        // Click text=Speichern >> nth=0
        await Page.Locator("text=Speichern").First.ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/wse/**/Der+Test");
        // Click text=Der Test
        await Page.Locator("text=Der Test").ClickAsync();
    }

    [TearDown]
    public async Task OneTimeTeardown() {
        // Go to https://localhost:7206
        await Page.GotoAsync("https://localhost:7206");
        // Click [aria-label="settings outline"]
        await Page.Locator("[aria-label=\"settings outline\"]").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/settings");
        // Click button:has-text("Konto löschen")
        await Page.Locator("button:has-text(\"Konto löschen\")").ClickAsync();
        // Click div[role="dialog"] button:has-text("Löschen")
        await Page.Locator("div[role=\"dialog\"] button:has-text(\"Löschen\")").ClickAsync();
        await Page.WaitForURLAsync("https://localhost:7206/");
    }
}
