using Microsoft.Playwright.NUnit;

namespace Microscope.Boilerplate.Clients.E2E;

public class AuthenticatedPageTest : PageTest
{
    [SetUp]
    public async Task SetUp()
    {
        await Page.GotoAsync("http://localhost:5215");
        await Page.Locator(".login-button").ClickAsync();
        await Page.TypeAsync("input[name='username']", "admin@microscope.io");
        await Page.TypeAsync("input[name='password']", "microscope");
        await Page.Locator("input[name='login']").ClickAsync();
    }
}