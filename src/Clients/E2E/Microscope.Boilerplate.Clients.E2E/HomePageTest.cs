using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Microscope.Boilerplate.Clients.E2E;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class HomePageTest : PageTest
{
    [Test]
    public async Task HomePage()
    {
        await Page.GotoAsync("http://localhost:5215");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Index"));
        var titleLocator = Page.GetByRole(AriaRole.Heading, new PageGetByRoleOptions() { Name = "Microscope" });
        var title2Locator = Page.GetByRole(AriaRole.Heading, new PageGetByRoleOptions() { Name = "Boilerplate" });

        await Expect(titleLocator).ToBeVisibleAsync();
        await Expect(title2Locator).ToBeVisibleAsync();
    }
}