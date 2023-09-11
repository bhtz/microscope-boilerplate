using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Microscope.Boilerplate.Clients.E2E;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class TodoListPageTest : AuthenticatedPageTest
{
    private const string TodoListUrl = "http://localhost:5215/todolist";
    private const string FakeTodoListName = "MCSP_FAKE_TODOLIST_NAME";
    private const string FakeTodoListNameUpdated = "MCSP_FAKE_TODOLIST_NAME_UPDATED";
    private const string FakeTodoListItemName = "ITEM_TEST";

    [Test, Order(1)]
    public async Task TodoListPage()
    {
        await Page.GotoAsync(TodoListUrl);
        await Expect(Page).ToHaveTitleAsync(new Regex("Liste des tâches"));
    }

    [Test, Order(2)]
    public async Task AddTodoList()
    {
        await Page.GotoAsync(TodoListUrl);
        await Page.Locator(".add-todolist").ClickAsync();
        await Page.Locator(".input-todolist-name input").FillAsync(FakeTodoListName);
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions() { Name = "Sauvegarder" }).ClickAsync();
        await Expect(Page).ToHaveTitleAsync(new Regex(FakeTodoListName));
    }

    [Test, Order(3)]
    public async Task Update()
    {
        await Page.GotoAsync(TodoListUrl);
        await Page.Locator(".mud-table-row b").GetByText(FakeTodoListName).First.ClickAsync();
        await Expect(Page).ToHaveTitleAsync(new Regex(FakeTodoListName));
        
        await Page.Locator(".update-todolist").ClickAsync();
        await Page.Locator(".input-todolist-name input").FillAsync(FakeTodoListNameUpdated);
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions() { Name = "Sauvegarder" }).ClickAsync();
        await Expect(Page).ToHaveTitleAsync(new Regex(FakeTodoListNameUpdated));
    }
    
    [Test, Order(4)]
    public async Task AddItem()
    {
        await Page.GotoAsync(TodoListUrl);
        await Page.Locator(".mud-table-row b").GetByText(FakeTodoListName).First.ClickAsync();
        await Expect(Page).ToHaveTitleAsync(new Regex(FakeTodoListName));

        await Page.Locator("input[inputmode=text]").First.FillAsync(FakeTodoListItemName);
        await Page.Keyboard.PressAsync("Enter");

        var itemLocator = Page.Locator(".mud-list-item .mud-typography").GetByText(FakeTodoListItemName).First;
        await Expect(itemLocator).ToBeVisibleAsync();
    }
}
