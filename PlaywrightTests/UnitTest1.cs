using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;

public class Tests : Pagetests {

    [Test]
    public async Task RegisterAndLogout() {

        // Go to https://localhost:7206/
        await page.GotoAsync("https://localhost:7206/");

        // Go to https://localhost:7206/register
        await page.GotoAsync("https://localhost:7206/register");

        // Fill #lastName
        await page.Locator("#lastName").FillAsync("Mustermann");

        // Fill #firstName
        await page.Locator("#firstName").FillAsync("Max");

        // Fill input[type="email"]
        await page.Locator("input[type=\"email\"]").FillAsync("testemail@mail.com");

        // Fill #password
        await page.Locator("#password").FillAsync("12345");

        // Fill #confirmPassword
        await page.Locator("#confirmPassword").FillAsync("12345");

        // Click button:has-text("Registrieren")
        await page.Locator("button:has-text(\"Registrieren\")").ClickAsync();
        await page.WaitForURLAsync("https://localhost:7206/");

        // Click [aria-label="person circle outline"]
        await page.Locator("[aria-label=\"person circle outline\"]").ClickAsync();

        // Click text=Abmelden
        await page.Locator("text=Abmelden").ClickAsync();
        await page.WaitForURLAsync("https://localhost:7206/");
    }
}
