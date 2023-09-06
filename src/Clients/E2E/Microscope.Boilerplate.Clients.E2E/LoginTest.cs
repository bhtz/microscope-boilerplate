using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Microscope.Boilerplate.Clients.E2E;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LoginPageTest : PageTest
{
    [Test]
    public async Task LoginAndRegisterPage()
    {
        await Page.GotoAsync("http://localhost:5215");
        await Page.Locator(".login-button").ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Regex(".*http://localhost:8083/"));

        // Login with user
        await Page.TypeAsync("input[name='username']", "admin@microscope.io");
        await Page.TypeAsync("input[name='password']", "microscope");

        await Page.Locator("input[name='login']").ClickAsync();

        var authCallbackPath = new Regex(".*http://localhost:5215/authentication/login-callback");
        await Expect(Page).ToHaveURLAsync(authCallbackPath);

        // login display with connected user and avatar corresponding to the mail
        var avatarSelector = Page.Locator(".mud-avatar:has-text('AD')");
        await Expect(avatarSelector).ToBeVisibleAsync();
    }
}